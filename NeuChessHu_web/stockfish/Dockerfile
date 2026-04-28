FROM php:8.3-cli

RUN apt-get update \
    && apt-get install -y --no-install-recommends stockfish \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/* /var/cache/apt/archives/*

WORKDIR /app

COPY StockfishEngineService.php index.php

EXPOSE 8001

CMD ["php", "-S", "0.0.0.0:8001", "index.php"]