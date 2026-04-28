<?php

namespace ChessLogic\MatchDatas\DataStores;

use ChessLogic\ChessBoard\ChessPieces\Definitions\Side;
use ChessLogic\MatchDatas\MatchDataStore;
use JsonSerializable;

class Clocks implements JsonSerializable
{
    public int $WhiteRemainingMs;
    public int $BlackRemainingMs;
    public int $IncrementMs;
    public int $LastMoveTimestampMs;

    public function __construct(int $baseMs, int $incrementMs)
    {
        $this->WhiteRemainingMs = $baseMs;
        $this->BlackRemainingMs = $baseMs;
        $this->IncrementMs = $incrementMs;
        $this->LastMoveTimestampMs = self::nowMs();
    }

    public function onMove(Side $movingSide): bool
    {
        $now = self::nowMs();
        $elapsed = $now - $this->LastMoveTimestampMs;

        if ($movingSide === Side::White)
        {
            $this->WhiteRemainingMs -= $elapsed;
            $this->WhiteRemainingMs += $this->IncrementMs;

            if ($this->WhiteRemainingMs <= 0)
            {
                $this->WhiteRemainingMs = 0;
                return false;
            }
        }
        else
        {
            $this->BlackRemainingMs -= $elapsed;
            $this->BlackRemainingMs += $this->IncrementMs;

            if ($this->BlackRemainingMs <= 0)
            {
                $this->BlackRemainingMs = 0;
                return false;
            }
        }

        $this->LastMoveTimestampMs = $now;
        return true;
    }

    public static function isTimeoutValid(MatchDataStore $store): bool
    {
        $now = self::nowMs();
        $elapsed = $now - $store->Clocks->LastMoveTimestampMs;

        $remaining = $store->MatchState->CurrentSide === Side::White
            ? $store->Clocks->WhiteRemainingMs - $elapsed
            : $store->Clocks->BlackRemainingMs - $elapsed;

        return $remaining <= 0;
    }

    public function jsonSerialize(): array
    {
        return [
            'whiteRemainingMs' => $this->WhiteRemainingMs,
            'blackRemainingMs' => $this->BlackRemainingMs,
            'incrementMs' => $this->IncrementMs,
            'lastMoveTimestampMs' => $this->LastMoveTimestampMs,
        ];
    }

    public static function fromArray(array $data): self
    {
        $clock = new self(0, 0);

        $clock->WhiteRemainingMs = $data['whiteRemainingMs'] ?? 0;
        $clock->BlackRemainingMs = $data['blackRemainingMs'] ?? 0;
        $clock->IncrementMs = $data['incrementMs'] ?? 0;
        $clock->LastMoveTimestampMs = $data['lastMoveTimestampMs'] ?? 0;

        return $clock;
    }

    public static function fromMatchDuration(string $matchDuration): self
    {
        $matchDuration = str_replace(' ', '', $matchDuration);

        if (str_contains($matchDuration, '|'))
        {
            [$minutes, $incrementSeconds] = explode('|', $matchDuration);
        }
        else
        {
            $minutes = $matchDuration;
            $incrementSeconds = 0;
        }

        $baseMs = (int)$minutes * 60 * 1000;
        $incrementMs = (int)$incrementSeconds * 1000;

        return new self($baseMs, $incrementMs);
    }

    public static function formatTime(int $remainingMs) : string
    {
        $totalSeconds = $remainingMs / 1000.0;
        $minutes = (int)($totalSeconds / 60);
        $rawSeconds = $totalSeconds % 60;
        $wholeSeconds = (int)floor($rawSeconds);

        if ($totalSeconds < 10)
        {
            $tenths = (int)floor($rawSeconds * 10) % 10;
            return sprintf("00:%02d.%d", $wholeSeconds, $tenths);
        }

        return sprintf("%02d:%02d", $minutes, $wholeSeconds);
    }

    private static function nowMs(): int
    {
        return (int)(microtime(true) * 1000);
    }
}