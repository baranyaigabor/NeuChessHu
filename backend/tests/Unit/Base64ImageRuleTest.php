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

    public function testValidPngPasses(): void
    {
        $pngData = 'iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+/p9sAAAAASUVORK5CYII=';
        $value = 'data:image/png;base64,' . $pngData;
        $errors = $this->validate($value);
        $this->assertEmpty($errors);
    }

    public function testMissingPrefixFails(): void
    {
        $errors = $this->validate(base64_encode('not-a-real-image'));
        $this->assertNotEmpty($errors);
        $this->assertStringContainsString('JPEG or PNG', $errors[0]);
    }

    public function testGifPrefixFails(): void
    {
        $errors = $this->validate('data:image/gif;base64,' . base64_encode('gif-data'));
        $this->assertNotEmpty($errors);
        $this->assertStringContainsString('JPEG or PNG', $errors[0]);
    }

    public function testInvalidBase64Fails(): void
    {
        $errors = $this->validate('data:image/png;base64,!!!NOTVALIDBASE64!!!');
        $this->assertNotEmpty($errors);
    }

    public function testOversizedImageFails(): void
    {
        $bigData = str_repeat('A', 2 * 1024 * 1024 + 1);
        $value = 'data:image/png;base64,' . base64_encode($bigData);

        $errors = $this->validate($value);

        $this->assertNotEmpty($errors);
        $this->assertStringContainsString('2MB', $errors[0]);
    }

    public function testValidBase64ButNotImageFails(): void
    {
        $value = 'data:image/png;base64,' . base64_encode('this is not an image');
        $errors = $this->validate($value);
        $this->assertNotEmpty($errors);
    }

    public function testJpegPrefixIsAcceptedFormat(): void
    {
        $jpegData = "\xFF\xD8\xFF\xE0\x00\x10JFIF\x00\x01\x01\x00\x00\x01\x00\x01\x00\x00"
                  . "\xFF\xDB\x00C\x00\x08\x06\x06\x07\x06\x05\x08\x07\x07\x07\t\t\x08\n\x0c\x14"
                  . "\r\x0c\x0b\x0b\x0c\x19\x12\x13\x0f\x14\x1d\x1a\x1f\x1e\x1d\x1a\x1c\x1c $.',"
                  . '"#\x1c\'25=82<.342\x1c\'...\xFF\xD9';
        $value = 'data:image/jpeg;base64,' . base64_encode($jpegData);
        $errors = $this->validate($value); 
        
        if (!empty($errors)) {
            $this->assertStringNotContainsString('JPEG or PNG', $errors[0]);
        }
    }
}