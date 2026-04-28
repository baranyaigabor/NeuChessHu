<?php

require '/www/vendor/autoload.php';

$app = require_once '/www/bootstrap/app.php';

use Illuminate\Foundation\Bootstrap\LoadEnvironmentVariables;
use Illuminate\Foundation\Bootstrap\LoadConfiguration;
use Illuminate\Foundation\Bootstrap\RegisterFacades;
use Illuminate\Foundation\Bootstrap\RegisterProviders;
use Illuminate\Foundation\Bootstrap\BootProviders;
use App\Services\StockfishService;

$app->bootstrapWith([
    LoadEnvironmentVariables::class,
    LoadConfiguration::class,
    RegisterFacades::class,
    RegisterProviders::class,
]);

config(['broadcasting.default' => 'null']);
config(['cache.default' => 'database']);
config(['cache.stores.database.connection' => 'mysql']);
config(['cache.prefix' => 'neuchesshu-cache-']);
config(['database.default' => 'mysql']);

$app->bootstrapWith([
    BootProviders::class,
]);

use Ratchet\RFC6455\Handshake\RequestVerifier;
use Ratchet\RFC6455\Handshake\ServerNegotiator;
use Ratchet\RFC6455\Messaging\MessageBuffer;
use Ratchet\RFC6455\Messaging\FrameInterface;
use Ratchet\RFC6455\Messaging\MessageInterface;
use React\EventLoop\Loop;
use React\Socket\SocketServer;
use React\Socket\ConnectionInterface;
use App\WebSocket\Engine\ChessEngine;
use ChessLogic\Messaging\ChatMessagesHandlerFactory;
use ChessLogic\Moving\Factories\MoveFactory;
use ChessLogic\Moving\Factories\MoveValidatorFactory;
use ChessLogic\Moving\Factories\MoveComponents\MoveComponentsFactory;
use GuzzleHttp\Psr7\HttpFactory;
use GuzzleHttp\Psr7\Message;
use Ratchet\RFC6455\Messaging\CloseFrameChecker;
use Ratchet\RFC6455\Messaging\Frame;

$componentsFactory = new MoveComponentsFactory();
$moveValidatorFactory = new MoveValidatorFactory();
$moveFactory = new MoveFactory($componentsFactory, $moveValidatorFactory);
$chatMessagesHandler = new ChatMessagesHandlerFactory();
$stockfishService = new StockfishService();
$chessEngine = new ChessEngine($moveFactory, $moveValidatorFactory, $componentsFactory, $chatMessagesHandler, $stockfishService);

$loop = Loop::get();
$socket = new SocketServer('0.0.0.0:7001', [], $loop);

$socket->on('connection', function (ConnectionInterface $conn) use ($chessEngine) { $buffer = '';
    $handshakeDone = false; $negotiator = new ServerNegotiator(new RequestVerifier(), new HttpFactory(),
    false);
    $msgBuffer = null;

    $conn->on('data', function ($data) use ($conn, $chessEngine, &$buffer, 
        &$handshakeDone, $negotiator, &$msgBuffer) 
    {
        $buffer .= $data;

        if (!$handshakeDone) {
            if (strpos($buffer, "\r\n\r\n") === false)
                return;

            $request = Message::parseRequest($buffer);
            $response = $negotiator->handshake($request);

            $conn->write(Message::toString($response));

            if ($response->getStatusCode() !== 101) {
                $conn->close();
                return;
            }

            $handshakeDone = true;
            $buffer = '';

            $msgBuffer = new MessageBuffer(new CloseFrameChecker(),
                function (MessageInterface $msg) use ($conn, $chessEngine) {
                    $response = $chessEngine->onMessage($msg->getPayload());
                    if ($response !== null) {
                        $frame = new Frame($response, true, Frame::OP_TEXT);
                        $conn->write($frame->getContents());
                    }
                },
                function (FrameInterface $frame) use ($conn) {
                    $conn->write($frame->getContents());
                },
                false, null, 0, 0, null, null
            );

            return;
        }

        if ($msgBuffer === null) return;
            $msgBuffer->onData($data);
    });
});

$loop->run();