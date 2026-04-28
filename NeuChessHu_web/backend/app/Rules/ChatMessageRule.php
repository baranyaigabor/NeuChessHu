<?php

namespace App\Rules;

use Closure;
use Illuminate\Contracts\Validation\ValidationRule;

class ChatMessageRule implements ValidationRule
{
    const MIN_LENGTH = 1;
    const MAX_LENGTH = 100;
    const ENGLISH_BANNED = [
        'fuck', 'f u c k', 'f*ck', 'fck',
        'shit', 'sh1t', 'sh!t',
        'ass', 'arse', 'asshole', 'arsehole',
        'bitch', 'b1tch', 'b!tch',
        'cunt', 'c*nt',
        'dick', 'd1ck',
        'cock', 'c0ck',
        'pussy', 'puss y',
        'whore', 'wh0re',
        'bastard',
        'nigger', 'nigga', 'n1gger', 'n!gger',
        'faggot', 'fag', 'f4ggot',
        'retard', 'ret4rd',
        'kike', 'spic', 'chink', 'gook', 'wetback',
        'rape', 'rapist',
        'kill yourself', 'kys',
        'go die', 'die bitch',
        'nazi', 'heil',
    ];
 
    const HUNGARIAN_BANNED = [
        'kurva', 'kurv4', 'k*rva', 'kurwa',
        'fasz', 'f4sz', 'f*sz',
        'geci', 'g3ci', 'g*ci',
        'bazmeg', 'b4zmeg', 'baszd meg', 'baszd',
        'basz', 'b4sz',
        'pina', 'p1na', 'p*na', 'pína',
        'segg', 's3gg', 's*gg',
        'szar', 'sz4r',
        'picsa', 'p1csa',
        ' anyád', 'anyad', 'anyadat', 'anyádat',
        'anyád kurva', 'anyád picsája',
        'köcsög', 'kocsog', 'k0csog',
        'buzi', 'buz1',
        'ribanc', 'r1banc',
        'rohadék', 'rohadek',
        'dög', 'dog',
        'pofád', 'pofad',
        'taknyos',
        'hülye', 'hulye',
        'idióta', 'idiota',
        'állat', 'allat',
        'cigány', 'cigany', 'c1gány',
        'zsidó', 'zsido', 'zs1dó',
        'büdös zsidó', 'büdös cigány',
        'fasiszta', 'f4siszta',
        'náci', 'naci',
        'menj meg', 'döglj meg', 'doglj meg',
        'megöllek', 'megollek',
        'ölj meg', 'olj meg',
        'agyonütlek',
        'szexuális',
    ];
 
    const PATTERNS = [
        '/f[\W_]*u[\W_]*c[\W_]*k/i',
        '/s[\W_]*h[\W_]*i[\W_]*t/i',
        '/b[\W_]*i[\W_]*t[\W_]*c[\W_]*h/i',
        '/c[\W_]*u[\W_]*n[\W_]*t/i',
        '/k[\W_]*u[\W_]*r[\W_]*v[\W_]*a/i',
        '/f[\W_]*a[\W_]*s[\W_]*z/i',
        '/g[\W_]*e[\W_]*c[\W_]*i/i',
        '/b[\W_]*a[\W_]*s[\W_]*z/i',
        '/s[\W_]*e[\W_]*g[\W_]*g/i',
        '/p[\W_]*i[\W_]*c[\W_]*s[\W_]*a/i',
        '/n[\W_]*i[\W_]*g[\W_]*g[\W_]*[ae]r?/i',
        '/k[\W_]*[oó][\W_]*c[\W_]*s[\W_]*[oó][\W_]*g/i',
    ];

    /**
     * Run the validation rule.
     *
     * @param  \Closure(string, ?string=): \Illuminate\Translation\PotentiallyTranslatedString  $fail
     */
    
    public function validate(string $attribute, mixed $value, Closure $fail): void
    {
        if (!is_string($value)) {
            $fail('The :attribute must be a string.');
            return;
        }
 
        $trimmed = trim($value);
 
        if (strlen($trimmed) < self::MIN_LENGTH) 
        {
            $fail('The :attribute cannot be empty.');
            return;
        }
 
        if (strlen($trimmed) > self::MAX_LENGTH) 
        {
            $fail('The :attribute cannot exceed ' . self::MAX_LENGTH . ' characters.');
            return;
        }
 
        $normalized = mb_strtolower($trimmed);
        $normalized = preg_replace('/\s+/', ' ', $normalized);
 
        $leetMap = [
            '0' => 'o', '1' => 'i', '3' => 'e',
            '4' => 'a', '5' => 's', '@' => 'a',
            '!' => 'i', '$' => 's', '+' => 't',
        ];
        $deLeeted = strtr($normalized, $leetMap);
 
        foreach (self::PATTERNS as $pattern) {
            if (preg_match($pattern, $deLeeted)) {
                $fail('The :attribute contains inappropriate language.');
                return;
            }
        }
 
        $allBanned = array_merge(self::ENGLISH_BANNED, self::HUNGARIAN_BANNED);
 
        foreach ($allBanned as $word) {
            $wordNormalized = mb_strtolower(trim($word));
 
            if (str_contains($wordNormalized, ' ')) 
            {
                if (str_contains($deLeeted, $wordNormalized) || str_contains($normalized, $wordNormalized)) {
                    $fail('The :attribute contains inappropriate language.');
                    return;
                }
            } 
            else 
            {
                $pattern = '/\b' . preg_quote($wordNormalized, '/') . '\b/u';
                if (preg_match($pattern, $deLeeted) || preg_match($pattern, $normalized)) {
                    $fail('The :attribute contains inappropriate language.');
                    return;
                }
            }
        }
 
        $letters = preg_replace('/[^a-zA-Z]/', '', $trimmed);
        if (strlen($letters) > 10) {
            $upperCount = strlen(preg_replace('/[^A-Z]/', '', $letters));
            $ratio = $upperCount / strlen($letters);
            if ($ratio > 0.7) {
                $fail('The :attribute contains excessive capitalization.');
                return;
            }
        }
 
        if (preg_match('/(.)\1{6,}/', $trimmed)) {
            $fail('The :attribute contains repeated characters.');
            return;
        }
 
        if (!preg_match('/\p{L}/u', $trimmed)) {
            $fail('The :attribute must contain readable text.');
            return;
        }
    }
}
