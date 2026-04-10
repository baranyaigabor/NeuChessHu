<?php

namespace App\Rules;

use Closure;
use Illuminate\Contracts\Validation\ValidationRule;
use Illuminate\Translation\PotentiallyTranslatedString;

class Base64ImageRule implements ValidationRule
{
    /**
     * Run the validation rule.
     *
     * @param  \Closure(string, ?string=): PotentiallyTranslatedString  $fail
     */
    public function validate(string $attribute, mixed $value, Closure $fail): void
    {
        if (!preg_match('/^data:image\/(jpe?g|png);base64,/', $value)) {
            $fail('The image must be a JPEG or PNG.');
            return;
        }

        $base64 = substr($value, strpos($value, ',') + 1);

        $decoded = base64_decode($base64, true);
        if ($decoded === false) {
            $fail('Invalid base64 image.');
            return;
        }

        if (strlen($decoded) > 2 * 1024 * 1024) {
            $fail('Image must not exceed 2MB.');
            return;
        }

        if (!@getimagesizefromstring($decoded)) {
            $fail('Invalid image content.');
            return;
        }
    }
}
