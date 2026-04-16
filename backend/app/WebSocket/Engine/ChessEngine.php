<?php

namespace App\WebSocket\Engine;

use App\Services\MatchService;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Piece;
use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\ChessBoard\ChessPieces\Pieces\ChessPiece;
use ChessLogic\MatchDatas\DataStores\Clocks;
use ChessLogic\MatchDatas\MatchDataStore;
use ChessLogic\Messaging\ChatMessagesHandlerFactory;
use ChessLogic\Moving\Factories\MoveFactory;
use ChessLogic\Moving\Factories\MoveValidatorFactory;
use ChessLogic\Moving\Factories\MoveComponents\MoveComponentsFactory;
use Exception;
use Throwable;

class ChessEngine
{
    public function __construct(private MoveFactory $moveFactory,
        private MoveValidatorFactory $moveValidatorFactory,
        private MoveComponentsFactory $componentsFactory,
        private ChatMessagesHandlerFactory $chatMessagesHandler
    ) {}

    private array $matchDataStores = [];
    private array $validators = [];

    private function methodsFactory(string $channel): array
    {
        if (!isset($this->validators[$channel]))
        {
            $data = MatchService::getMatchFromCache($channel);

            if (!$data)
                throw new Exception(message: "Game not found for channel: $channel");

            $matchDataStore = MatchDataStore::fromCache($data);
            $components = $this->componentsFactory->create();

            $this->matchDataStores[$channel] = $matchDataStore;
            $this->validators[$channel] = $this->moveValidatorFactory->create($matchDataStore, $components);
        }

        $matchDataStore = $this->matchDataStores[$channel];
        $moveValidator = $this->validators[$channel];
        $move = $this->moveFactory->create($matchDataStore, $moveValidator);
        $chatMessage = $this->chatMessagesHandler->create($matchDataStore);

        return [$matchDataStore, $moveValidator, $move, $chatMessage];
    }
}