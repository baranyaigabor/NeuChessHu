<?php

namespace App\Rules;

use Closure;
use Illuminate\Contracts\Validation\ValidationRule;
use Illuminate\Translation\PotentiallyTranslatedString;

class ClamAVScanRule implements ValidationRule
{
    /**
     * Run the validation rule.
     *
     * @param  \Closure(string, ?string=): PotentiallyTranslatedString  $fail
     */
    public function validate(string $attribute, mixed $value, Closure $fail): void
    {
        if (!preg_match('/^data:image\/(jpeg|png);base64,/', $value, $matches)) {
            $fail('The image must be a JPEG or PNG.');
            return;
        }

        $decoded = base64_decode(substr($value, strpos($value, ',') + 1), true);
        if ($decoded === false) {
            $fail('Invalid base64 image.');
            return;
        }

        $stream = fopen('php://memory', 'r+b');
        fwrite($stream, $decoded);
        rewind($stream);

        $socket = @fsockopen("clamav", 3310, $errno, $errstr, 2);
        if (!$socket) {
            fclose($stream);
            $fail('Antivirus unavailable.');
            return;
        }

        stream_set_timeout($socket, 5);
        fwrite($socket, "zINSTREAM\0");

        while (!feof($stream)) {
            $chunk = fread($stream, 8192);
            $size = pack('N', strlen($chunk));
            fwrite($socket, $size . $chunk);
        }
        fclose($stream);

        fwrite($socket, pack('N', 0));

        $response = '';
        while (!feof($socket)) {
            $response .= fgets($socket);
        }
        fclose($socket);

        if (preg_match('/FOUND$/m', $response)) {
            $fail('File is infected.');
        }
    }
}
