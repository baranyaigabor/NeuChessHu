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
- **TC-031** (Téma váltás dark/light)
  A teszt azt ellenőrzi, hogy a téma gomb váltja az oldal színvilágát.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-032** (Bejelentkezés hálózati hiba)
  A teszt azt ellenőrzi, hogy hálózati hiba esetén általános hibaüzenet jelenik meg.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-033** (Sikeres bejelentkezés adminként)
  A teszt azt ellenőrzi, hogy admin szerepkörrel admin oldalra navigál.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-034** (redirect_uri kezelése login során)
  A teszt azt ellenőrzi, hogy redirect_uri paraméter esetén OAuth close URL hívódik.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-035** (Regisztráció 1. lépés email validáció)
  A teszt azt ellenőrzi, hogy hibás emailnél nem aktív a továbblépés.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-036** (Regisztráció 1. lépés jelszó erősség)
  A teszt azt ellenőrzi, hogy gyenge jelszónál validációs hiba jelenik meg.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-037** (Regisztráció 1. lépés jelszó megerősítés)
  A teszt azt ellenőrzi, hogy nem egyező jelszavaknál hiba látható.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-038** (Regisztráció 1. lépés sikeres továbblépés)
  A teszt azt ellenőrzi, hogy érvényes adatokkal 2. lépésre navigál.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-039** (Stepper 2. lépés tiltás hiányos 1. lépésnél)
  A teszt azt ellenőrzi, hogy a 2. lépés csak érvényes 1. lépés után kattintható.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-040** (Regisztráció 2. lépés nickname kötelező)
  A teszt azt ellenőrzi, hogy üres 'nickname' esetén validációs hiba jelenik meg.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-041** (Regisztráció 2. lépés nickname szabályok)
  A teszt azt ellenőrzi, hogy tiltott vagy túl rövid formátumot elutasít.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-042** (Regisztráció 2. lépés opcionális keresztnév)
  A teszt azt ellenőrzi, hogy üres keresztnév engedélyezett.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-043** (Regisztráció 2. lépés opcionális vezetéknév)
  A teszt azt ellenőrzi, hogy üres vezetéknév engedélyezett.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-044** (Regisztráció 2. lépés régió kiválasztás)
  A teszt azt ellenőrzi, hogy a kiválasztott régió mentésre kerül.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-045** (Regisztráció 2. lépés születési dátum validáció)
  A teszt azt ellenőrzi, hogy jövőbeli dátum megadása tiltott.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-046** (Regisztráció 2. lépés Előző gomb)
  A teszt azt ellenőrzi, hogy visszalépéskor az addig kitöltött adatok megmaradnak.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-047** (Regisztráció 2. lépés Következő gomb)
  A teszt azt ellenőrzi, hogy érvényes adatokkal 3. lépésre navigál.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-048** (Stepper 3. lépés tiltás hiányos 2. lépésnél)
  A teszt azt ellenőrzi, hogy a 3. lépés csak kitöltött nickname után kattintható.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-049** (Regisztráció 3. lépés összegző adatok)
  A teszt azt ellenőrzi, hogy a megadott email és profiladatok helyesen jelennek meg.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-050** (Regisztráció 3. lépés ÁSZF kötelező)
  A teszt azt ellenőrzi, hogy áSZF elfogadás nélkül nincs sikeres beküldés.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-051** (Regisztráció 3. lépés ÁSZF link)
  A teszt azt ellenőrzi, hogy a link új lapon nyílik locale-függő URL-re.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-052** (Regisztráció 3. lépés szerver hiba)
  A teszt azt ellenőrzi, hogy aPI hiba esetén backend üzenet vagy fallback hiba jelenik meg.. A várt kimenet: sikeres futás és helyes viselkedés.
