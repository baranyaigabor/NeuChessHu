# NeuChessHu Documentation Package

This folder contains the generated English developer documentation and user documentation for NeuChessHu.

## Developer Documentation

Main file:

- [developer-documentation.md](./NeuChessHu_web/docs/content/en/developer-documentation.md)

Important sections:

- [Zero-to-Running Checklist](./NeuChessHu_web/docs/content/en/developer-documentation.md#61-zero-to-running-checklist)
- [Environment Configuration](./NeuChessHu_web/docs/content/en/developer-documentation.md#7-environment-configuration)
- [Pusher/Soketi Values](./NeuChessHu_web/docs/content/en/developer-documentation.md#71-pushersoketi-values)
- [Seeded Users](./NeuChessHu_web/docs/content/en/developer-documentation.md#91-seeded-users)
- [Backend API](./NeuChessHu_web/docs/content/en/developer-documentation.md#10-backend-api)
- [Testing](./NeuChessHu_web/docs/content/en/developer-documentation.md#19-testing)
- [Build and Publish](./NeuChessHu_web/docs/content/en/developer-documentation.md#20-build-and-publish)

## User Documentation

- [USER_DOCUMENTATION.md](./docs/content/en/user-guide.md.md)
- [USER_DOCUMENTATION_HU.md](./docs/content/hu/user-guide.md)

## Quick Start for the Web Project

```bash
cd /Users/boss/Downloads/NeuChessHu-web
./start.sh
docker compose exec backend php artisan db:seed --class=UserSeeder
```

See the full startup instructions in the [Zero-to-Running Checklist](./NeuChessHu_web/docs/content/en/developer-documentation.md#61-zero-to-running-checklist).

## Seeded Users

The seeded users and test credentials are listed in [Seeded Users](./NeuChessHu_web/docs/content/en/developer-documentation.md#91-seeded-users).

## Tests and Test Records

Test execution commands and the locations of the test documentation and test record workbooks are listed in [Testing](./NeuChessHu_web/docs/content/en/developer-documentation.md#19-testing).