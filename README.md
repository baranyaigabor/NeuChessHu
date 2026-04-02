# Fullstack 2025

Figyelem! Mivel a Docker Compose és több container is igényt tart a `.env` fájlra, így az eredetileg a backendben lévő `.env.example` a projekt gyökerében található. Ezt kell lemásolni `.env` néven, majd igény szerint beállítani. Ezután a Docker Compose felcsatolja a szükséges containerekhez. 
## Indítás

A rendszer inicializálását és az első indítását a `start.sh` script végzi.

```bash
./start.sh
```

## Leállítás

```bash
docker compose stop
```

## Eltávolítás

```bash
docker compose down -v
```

 - A `-v` hatására a volume-okat is törli, így az adatbázisban tárolt adatok elvesznek.