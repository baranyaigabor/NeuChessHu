<?php

return [
    'paths' => ['api/*', 'signin', 'sanctum/csrf-cookie', 'signup'],
    'allowed_methods' => ['*'],
    'allowed_origins' => ['http://frontend.vm2.test'],
    'allowed_headers' => ['*'],
    'supports_credentials' => true,
];