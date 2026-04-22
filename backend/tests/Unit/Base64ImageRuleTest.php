<?php

namespace Tests\Unit;

use App\Rules\Base64ImageRule;
use PHPUnit\Framework\TestCase;

class Base64ImageRuleTest extends TestCase
{
    private Base64ImageRule $rule;

    protected function setUp(): void
    {
        $this->rule = new Base64ImageRule();
    }

    private function validate(mixed $value): array
    {
        $errors = [];
        $fail = function (string $msg) use (&$errors) {
            $errors[] = $msg;
        };
        $this->rule->validate('profile_picture', $value, $fail);
        return $errors;
    }
}