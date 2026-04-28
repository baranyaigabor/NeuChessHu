<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Authentication Successful</title>
    <style>
        body {
            font-family: system-ui, -apple-system, sans-serif;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
            background: #f5f5f5;
        }
        .container {
            text-align: center;
            padding: 2rem;
            background: white;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }
        .success {
            color: #10b981;
            font-size: 3rem;
            margin-bottom: 1rem;
        }
        h1 {
            color: #1f2937;
            margin-bottom: 0.5rem;
        }
        p {
            color: #6b7280;
        }
    </style>
</head>
<body>
    <div class="container">
        <div class="success">✓</div>
        <h1>Authentication Successful</h1>
        <p>You can close this window now.</p>
    </div>

    <script>
        window.location.href = "neuchesshu://auth/callback?data={{ $payload }}";
    </script>
</body>
</html>