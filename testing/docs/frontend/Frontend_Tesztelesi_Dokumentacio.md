# NeuChess Frontend - Tesztelési Dokumentáció

## Összefoglaló

- Tesztesetek száma: 52
- Sikeres tesztek: 52

## Tesztek lényege

- **TC-001** (Nyitóoldal betöltése)
  A teszt azt ellenőrzi, hogy a kezdőoldal renderelődik törött elem nélkül.. A várt kimenet: sikeres futás és helyes viselkedés.

- **TC-002** (Navigációs sáv logó megjelenése)
  A teszt azt ellenőrzi, hogy a NeuChess logó minden publikus oldalon látható.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-003** (Nyelvváltó gomb működése)
  A teszt azt ellenőrzi, hogy hU/EN váltásra a feliratok nyelve frissül.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-004** (Téma mentés localStorage-ban)
  A teszt azt ellenőrzi, hogy oldalfrissítés után a választott téma megmarad.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-005** (Mobil hamburger menü nyitás)
  A teszt azt ellenőrzi, hogy kis képernyőn a menü gomb megnyitja a mobil menüt.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-006** (Mobil hamburger menü zárás)
  A teszt azt ellenőrzi, hogy új kattintásra a mobil menü bezáródik.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-007** (Sign in oldal elérhetősége)
  A teszt azt ellenőrzi, hogy a bejelentkezési oldal route-ról betöltődik.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-008** (Sign up oldal elérhetősége)
  A teszt azt ellenőrzi, hogy a regisztráció első lépése route-ról betöltődik.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-009** (Bejelentkezés üres mezők validáció)
  A teszt azt ellenőrzi, hogy kötelező mezők hiányára validációs üzenet jelenik meg.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-010** (Bejelentkezés hibás email formátum)
  A teszt azt ellenőrzi, hogy rossz email formátum esetén nem enged tovább.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-011** (Bejelentkezés hibás jelszó hiba)
  A teszt azt ellenőrzi, hogy 401 esetén rossz hitelesítési üzenet látható.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-012** (Sikeres bejelentkezés felhasználóként)
  A teszt azt ellenőrzi, hogy siker esetén a user profil oldal nyílik meg.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-013** (Jelszó mező megjelenítés kapcsoló)
  A teszt azt ellenőrzi, hogy a jelszó láthatóság kapcsolható.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-014** (Regisztráció sikeres befejezés)
  A teszt azt ellenőrzi, hogy siker után automatikusan beléptet és profilra visz.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-015** (Regisztráció adatok resetje siker után)
  A teszt azt ellenőrzi, hogy sikeres regisztráció után a store adatai ürülnek.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-016** (Dinamikus user route betöltés)
  A teszt azt ellenőrzi, hogy a /user/:nickname oldal a megfelelő profilt tölti be.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-017** (Profil oldal cím frissítése)
  A teszt azt ellenőrzi, hogy a böngészőfül címe a nickname-re vált.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-018** (Profil statisztika panel megjelenése)
  A teszt azt ellenőrzi, hogy personal stat panel helyesen renderelődik.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-019** (Meccs információk panel megjelenése)
  A teszt azt ellenőrzi, hogy match infos panel megjelenik és adatot mutat.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-020** (Sakk statisztika panel megjelenése)
  A teszt azt ellenőrzi, hogy chess stat panel megjelenik és adatot mutat.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-021** (Saját profil szerkesztés engedélyezés)
  A teszt azt ellenőrzi, hogy tulajdonosként a profilmezők szerkeszthetők.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-022** (Idegen profil szerkesztés tiltás)
  A teszt azt ellenőrzi, hogy nem tulajdonosként szerkesztés nem érhető el.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-023** (Profil mentés)
  A teszt azt ellenőrzi, hogy mentés után új adatok töltődnek vissza a felületen.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-024** (Profil törlés saját fiókkal)
  A teszt azt ellenőrzi, hogy saját fiók törlése után kijelentkezik és sign inre visz.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-025** (Logout gomb működése)
  A teszt azt ellenőrzi, hogy kijelentkezés törli a session adatokat és visszairányít.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-026** (Admin oldal elérhetősége adminnak)
  A teszt azt ellenőrzi, hogy adminként az admin lista oldal betöltődik.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-027** (Admin user lista megjelenítés)
  A teszt azt ellenőrzi, hogy a felhasználói kártyák listázása működik.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-028** (Admin szerkesztés felhasználón)
  A teszt azt ellenőrzi, hogy admin mentés frissíti az adott felhasználó kártyáját.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-029** (Admin törlés felhasználón)
  A teszt azt ellenőrzi, hogy admin törlés után a törölt user eltűnik a listából.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-030** (Oldalcím lokalizált route-okon)
  A teszt azt ellenőrzi, hogy route váltáskor lokalizált page title jelenik meg.. A várt kimenet: sikeres futás és helyes viselkedés.
