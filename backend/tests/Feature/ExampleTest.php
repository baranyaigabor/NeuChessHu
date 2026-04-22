<?php

namespace Tests\Feature;

use Tests\TestCase;

class ExampleTest extends TestCase
{
    public function testDesktopAuthorizeRedirectsGuestsToSignIn() : void
    {
        $response = $this->get('/desktop/authorize?redirect_uri=neuchesshu://callback');

        $response->assertRedirect('http://frontend.vm2.test/signin?redirect_uri=neuchesshu%3A%2F%2Fcallback');
    }
}