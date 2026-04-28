<?php

namespace Tests\Unit;

use App\Rules\ChatMessageRule;
use PHPUnit\Framework\TestCase;

class ChatMessageRuleTest extends TestCase
{
    private ChatMessageRule $rule;
    private array $errors;

    protected function setUp(): void
    {
        $this->rule = new ChatMessageRule();
        $this->errors = [];
    }

    private function validate(mixed $value): array
    {
        $errors = [];
        $fail = function (string $msg) use (&$errors) {
            $errors[] = $msg;
        };
        $this->rule->validate('message', $value, $fail);
        return $errors;
    }

    public function testValidMessagePasses(): void
    {
        $this->assertEmpty($this->validate('Hello, good game!'));
    }

    public function testSingleCharacterPasses(): void
    {
        $this->assertEmpty($this->validate('a'));
    }

    public function testNonStringFails(): void
    {
        $errors = $this->validate(42);
        $this->assertNotEmpty($errors);
    }

    public function testEmptyStringFails(): void
    {
        $errors = $this->validate('');
        $this->assertNotEmpty($errors);
    }

    public function testWhitespaceOnlyFails(): void
    {
        $errors = $this->validate('   ');
        $this->assertNotEmpty($errors);
    }

    public function test101CharactersFails(): void
    {
        $errors = $this->validate(str_repeat('a', 101));
        $this->assertNotEmpty($errors);
    }

    public function testEnglishBannedWordFails(): void
    {
        $errors = $this->validate('you are a fuck');
        $this->assertNotEmpty($errors);
        $this->assertStringContainsString('inappropriate', $errors[0]);
    }

    public function testHungarianBannedWordFails(): void
    {
        $errors = $this->validate('te k*rva');
        $this->assertNotEmpty($errors);
        $this->assertStringContainsString('inappropriate', $errors[0]);
    }

    public function testLeetSpeakBannedWordFails(): void
    {
        $errors = $this->validate('you f4ggot');
        $this->assertNotEmpty($errors);
    }

    public function testPatternBasedBannedWordFails(): void
    {
        $errors = $this->validate('f*ck you');
        $this->assertNotEmpty($errors);
    }

    public function testExcessiveCapitalizationFails(): void
    {
        $errors = $this->validate('HELLO WORLDa');
        $this->assertNotEmpty($errors);
        $this->assertStringContainsString('capitalization', $errors[0]);
    }

    public function testNormalCapitalizationPasses(): void
    {
        $errors = $this->validate('Hello World, nice game today');
        $this->assertEmpty($errors);
    }

    public function testRepeatedCharactersFail(): void
    {
        $errors = $this->validate('hahahahahahahahaha aaaaaaa');
        $this->assertNotEmpty($errors);
        $this->assertStringContainsString('repeated', $errors[0]);
    }

    public function testSixRepeatedCharactersPasses(): void
    {
        $errors = $this->validate('haaaaaa');
        $this->assertEmpty($errors);
    }

    public function testNoLettersFails(): void
    {
        $errors = $this->validate('12345 !!!');
        $this->assertNotEmpty($errors);
        $this->assertStringContainsString('readable', $errors[0]);
    }

    public function testMessageWithNumbersAndLetterPasses(): void
    {
        $errors = $this->validate('gg wp 10/10');
        $this->assertEmpty($errors);
    }
}