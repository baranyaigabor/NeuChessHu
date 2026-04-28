<?php

return [

    'default' => env('BROADCAST_DRIVER', 'pusher'),

    'connections' => [

    'pusher' => [
        'driver' => 'pusher',
        'key' => env('PUSHER_APP_KEY'),
        'secret' => env('PUSHER_APP_SECRET'),
        'app_id' => env('PUSHER_APP_ID'),
        'options' => [
            'cluster' => env('PUSHER_APP_CLUSTER', 'eu'),
            'host' => env('PUSHER_HOST', '10.1.3.33'),
            'port' => env('PUSHER_PORT', 6001),
            'scheme' => 'http',
            'useTLS' => false,
            'encrypted' => false,
        ],
    ],

        'log' => [
            'driver' => 'log',
        ],

        'null' => [
            'driver' => 'null',
        ],
    ],
];