<?php

$method = $_SERVER['REQUEST_METHOD'];
$path = parse_url($_SERVER['REQUEST_URI'], PHP_URL_PATH);

if ($method === 'POST' && $path === '/bestmove') 
{
    $body = json_decode(file_get_contents('php://input'), true);
    $fen = $body['fen'] ?? null;
    $depth = $body['depth'] ?? $body['level'] ?? 10;

    if (!$fen)
    {
        http_response_code(422);
        json(['error' => 'Missing FEN']);
        exit;
    }

    $depth = max(1, min(20, (int)$depth));
    $move  = getBestMove($fen, $depth);

    if (!$move) 
    {
        http_response_code(500);
        json(['error' => 'No move found']);
        exit;
    }

    json(['move' => $move]);
    exit;
}

http_response_code(404);
json(['error' => 'Not found']);

function getBestMove(string $fen, int $depth) : ?string
{
    $descriptors = [
        0 => ['pipe', 'r'],
        1 => ['pipe', 'w'],
        2 => ['pipe', 'w'],
    ];

    $binary = stockfishBinary();

    if (!$binary)
    {
        return null;
    }

    $process = proc_open($binary, $descriptors, $pipes);

    if (!is_resource($process))
    {
        return null;
    }

    $commands = implode("\n", [
        'uci',
        'isready',
        "position fen {$fen}",
        "go depth {$depth}",
    ]);

    fwrite($pipes[0], $commands . "\n");

    $move = null;

    while (!feof($pipes[1])) 
    {
        $line = fgets($pipes[1]);

        if (str_starts_with($line, 'bestmove')) 
        {
            $parts = explode(' ', trim($line));
            $move  = $parts[1] ?? null;
            break;
        }
    }

    fwrite($pipes[0], "quit\n");
    fclose($pipes[0]);
    fclose($pipes[1]);
    fclose($pipes[2]);
    proc_close($process);

    return $move === '(none)' ? null : $move;
}

function stockfishBinary() : ?string
{
    $candidates = [
        getenv('STOCKFISH_BINARY') ?: null,
        '/usr/bin/stockfish',
        '/usr/games/stockfish',
        '/usr/local/bin/stockfish',
    ];

    foreach ($candidates as $candidate)
    {
        if ($candidate && is_executable($candidate))
        {
            return $candidate;
        }
    }

    return null;
}

function json(array $data) : void
{
    header('Content-Type: application/json');
    echo json_encode($data);
}