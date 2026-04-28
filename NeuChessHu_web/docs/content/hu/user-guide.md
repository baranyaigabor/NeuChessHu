# NeuChessHu Felhasználói Dokumentáció

## 1. Áttekintés

A 'NeuChessHu' projekt legfőbb célja, hogy a legelső, magyar származású sakkprogramot szolgáltassuk a nagyvilágnak, melyet elsősorban magyar nyelven, de nemzetközileg angol nyelven is lehet használni.

A 'NeuChessHu' egy sakkplatform, amely két fő részből áll:

- **Weboldal**: regisztrációra, bejelentkezésre, fiókkezelésre, profiloldalakra, játékosstatisztikákra, meccselőzményekre, letöltésekre és adminisztrációra használható.
- **Asztali alkalmazás**: sakkmeccsek lejátszására használható más játékosok vagy a Stockfish ellen, valamint játék közbeni beállításokra, chatre és eredmények megtekintésére.

A weboldal és az asztali alkalmazás ugyanazt a 'NeuChessHu' fiókot használja. A weboldalon létrehozott fiókkal az asztali alkalmazásban is be lehet jelentkezni.

## 2. Weboldal

### 2.1 Kezdőoldal

A kezdőoldal bemutatja a 'NeuChessHu'-t, és innen érhetők el a platform fő funkciói.

Elérhető műveletek:

- **Get Started / Kezdés**: megnyitja a bejelentkezési oldalt.
- **Download / Letöltés**: megnyitja az asztali alkalmazás letöltési menüjét.
- **Nyelvváltás**: a weboldal nyelvének váltása angol és magyar között.
- **Témaváltás**: világos és sötét mód közötti váltás.

Mobilnézetben ezek a műveletek a navigációs sáv menügombjából érhetők el.

### 2.2 Az asztali alkalmazás letöltése

A weboldalon a **Download / Letöltés** menüből tölthető le a Windows asztali alkalmazás.

Elérhető verziók:

- **Windows x64**
- **Windows ARM64**

A számítógépnek megfelelő verziót kell választani. A legtöbb hagyományos Windows számítógépen az x64 verzió használható.

### 2.3 Fiók létrehozása

A regisztráció három lépésből áll.

#### 1. lépés: Regisztráció

Megadandó adatok:

- e-mail cím
- jelszó
- jelszó megerősítése

A weboldal ellenőrzi, hogy:

- az e-mail cím megfelelő formátumú-e
- a jelszó eléri-e a szükséges hosszúságot
- a jelszó tartalmaz-e betűt és számot is
- a jelszó és a jelszó megerősítés megegyezik-e

Az adatok kitöltése után a **Next / Következő** gombbal lehet továbblépni.

#### 2. lépés: Személyes adatok

Megadandó profiladatok:

- becenév
- keresztnév
- vezetéknév
- régió/ország
- születési dátum

A becenév a platformon megjelenő nyilvános név. Ha a becenév már foglalt, a weboldal hibát jelez a regisztráció során.

A keresztnév, vezetéknév, régió és születési dátum a mentett profiladatoktól függően megjelenhet a felhasználói profiloldalon.

#### 3. lépés: Megerősítés

Ellenőrizni kell a regisztrációs adatokat, majd el kell fogadni a **Felhasználási feltételeket**.

A fiók nem hozható létre a feltételek elfogadása nélkül. A megerősítés után a weboldal létrehozza a fiókot, bejelentkezteti a felhasználót, és megnyitja az új profiloldalt.

### 2.4 Bejelentkezés

A bejelentkezési oldalon megadandó adatok:

- e-mail cím
- jelszó

Sikeres bejelentkezés után:

- a normál felhasználók a saját profiloldalukra kerülnek
- az admin felhasználók az adminisztrációs oldalra kerülnek

Hibás adatok esetén a weboldal érvénytelen e-mail vagy jelszó üzenetet jelenít meg.

### 2.5 Kijelentkezés

A navigációs sávban található **Log out / Kijelentkezés** gombbal lehet kijelentkezni.

Kijelentkezés után a weboldal visszairányít a bejelentkezési oldalra.

### 2.6 Profiloldal

Minden felhasználónak van nyilvános profiloldala.

A profiloldalon megjelenhet:

- becenév
- profilkép
- keresztnév
- vezetéknév
- régió
- születési dátum
- regisztráció dátuma
- online/offline állapot
- személyes meccsstatisztikák
- meccselőzmények
- sakkstatisztikák

A meccsek résznél láthatók a lejátszott játszmák, például az ellenfél, az eredmény, a játékmód, a dátum és ahol elérhető, a meccs befejezésének oka.

### 2.7 Saját profil szerkesztése

A saját profiloldalon a profilkártyán található beállítás/szerkesztés ikonnal lehet szerkeszteni az adatokat.

Szerkeszthető adatok:

- becenév
- keresztnév
- vezetéknév
- régió
- születési dátum
- profilkép

Profilkép feltölthető fájl kiválasztásával vagy kép behúzásával. A weboldal képfájlokat fogad el, és mentés előtt tömöríti őket. A túl nagy képek feldolgozás után elutasításra kerülhetnek.

Elérhető műveletek:

- **Save / Mentés**: módosítások mentése
- **Cancel / Mégse**: módosítások elvetése
- **Delete account / Fiók törlése**: fiók törlése megerősítés után

### 2.8 Fiók törlése

A saját profil szerkesztési módjában választható a **Delete account / Fiók törlése** művelet.

A weboldal megerősítést kér a fiók törlése előtt. Törlés után a felhasználók kijelentkeznek, és visszakerülnek a bejelentkezési oldalra.

### 2.9 Meccselőzmények és statisztikák

A profiloldal meccsekhez kapcsolódó adatokat is tartalmaz.

A meccselőzményekben megjelenhet:

- ellenfél
- eredmény: győzelem, vereség vagy döntetlen
- játékmód: Bullet, Blitz vagy Rapid
- meccs dátuma
- befejezési ok, például sakkmatt, időtúllépés, feladás, patt vagy közös megegyezés

A statisztikák tartalmazhatják:

- kedvenc játéktípus
- legtöbbet játszott időtartam
- kedvenc első lépés
- átlagos győzelmi idő
- győzelem/vereség/döntetlen diagramok
- meccsidővonal

Ha a felhasználónak még nincs meccse, a profil jelzi, hogy nincs találat.

### 2.10 Adminisztrációs oldal

Az admin felhasználók bejelentkezés után az adminisztrációs oldalra kerülnek.

Az adminisztrációs oldal felhasználólistát jelenít meg, és lehetővé teszi a felhasználói profilok kezelését, például:

- felhasználói profiladatok megtekintése
- felhasználói adatok szerkesztése
- profilképek módosítása
- felhasználók törlése

A Stockfish rendszerfelhasználó nem jelenik meg a látható felhasználólistában.

## 3. Asztali alkalmazás

### 3.1 Az asztali alkalmazás célja

Az asztali alkalmazás a sakkjátszmák fő felülete. Támogatott funkciók:

- játékosok közötti párkeresés
- játék Stockfish ellen
- Bullet, Blitz és Rapid időkontrollok
- élő sakktábla-kezelés
- lépésjelölések
- sakkórák
- leütött bábuk
- chat játék közben
- döntetlenajánlat, feladás, korai játszma megszakítása és eredményképernyő
- nyelv, téma, tábla, bábu, hang és automatikus vezérre váltás beállításai

### 3.2 Bejelentkezés az asztali alkalmazásból

Legtöbb asztali funkciókhoz bejelentkezés szükséges.

Ha bejelentkezésre van szükség, az alkalmazás böngészőben nyitja meg a weboldal bejelentkezési folyamatát. Sikeres bejelentkezés után a böngésző visszairányít az asztali alkalmazásba a NeuChessHu callback segítségével.

Ha a felhasználó bejelentkezés nélkül próbál használni egy védett funkciót, az alkalmazás bejelentkezési értesítést jelenít meg.

### 3.3 Főmenü

Az asztali főmenüben a sakktábla előnézete és a meccsindító vezérlők láthatók.

Fő műveletek:

- **Start Game / Játék indítása**: párkeresés indítása másik játékos ellen.
- **Időválasztó**: időkontroll kiválasztása.
- **More / Több**: további opciók megnyitása.
- **Play against Stockfish / Játék Stockfish ellen**: meccs indítása a Stockfish sakkmotor ellen.
- **Settings / Beállítások**: alkalmazásbeállítások megnyitása.
- **Profile Settings / Profilbeállítások**, **Logout / Kijelentkezés**, **Quit / Kilépés**: a menü felugró ablakából érhetők el.

### 3.4 Időkontroll kiválasztása

Az időválasztó ablakban lehet kiválasztani a meccs hosszát.

Elérhető kategóriák:

- **Bullet**
  - 1 perc
- **Blitz**
  - 3 perc
  - 5 perc
- **Rapid**
  - 10 perc
  - 15 perc

A kiválasztott időtartam mentésre kerül, és megjelenik a főmenü gombján.

### 3.5 Meccs indítása másik játékos ellen

Párkeresés indítása:

1. Jelentkezz be az asztali alkalmazásba.
2. Válassz időkontrollt.
3. Válaszd a **Start Game / Játék indítása** lehetőséget.
4. Várj a **Looking for Match / Meccs keresése** képernyőn.

A meccskereső képernyőn látható:

- kiválasztott meccsidő
- eltelt keresési idő
- váltakozó keresési üzenetek
- keresés leállítása

Ha az alkalmazás ellenfelet talál, a meccs automatikusan elindul.

### 3.6 Játék Stockfish ellen

Stockfish elleni játék indítása:

1. Nyisd meg a **More / Több** menüt a főmenüben.
2. Válaszd a **Play against Stockfish / Játék Stockfish ellen** lehetőséget.

A Stockfish elleni játszmák az asztali sakktáblán és meccsfelületen futnak. Stockfish ellen a chat és a döntetlenajánlat nem érhető el.

### 3.7 Sakktábla használata

Meccs közben a tábla megjeleníti:

- a bábukat
- a mezőkoordinátákat
- a játékos oldalának megfelelő tájolást, attól függően, hogy a felhasználó világossal vagy sötéttel játszik
- az aktuális táblatémát és bábutémát

A bábuk a táblán történő interakcióval mozgathatók. A szabálytalan lépéseket a játéklogika elutasítja.

Ha egy gyalog eléri az átváltozási sort, megjelenik az átváltozási ablak, kivéve ha az 'automatikus vezér' beállítás aktív.

### 3.8 Gyalog átváltozása

Átváltozáskor az alábbi bábuk közül lehet választani:

- vezér
- bástya
- futó
- huszár

Ha az **Auto-Queen / Automatikus vezér** beállítás aktív, az alkalmazás automatikusan vezért választ.

### 3.9 Meccs oldalsáv

A meccs oldalsávja élő információkat jelenít meg:

- játékos beceneve és profilképe
- ellenfél beceneve és profilképe
- játékos órája
- ellenfél órája
- leütött bábuk
- anyagi pontkülönbség
- lépésjelölések
- chat panel, ha elérhető

A lépésjelölések panelje a játszma során folyamatosan frissül.

### 3.10 Chat

A chat normál, játékosok közötti meccseken érhető el.

Chat használata:

1. Nyisd meg a chat panelt a meccs oldalsávjából.
2. Írd be az üzenetet.
3. Küldd el az üzenetet.

Ha a chat panel zárva van, és új üzenet érkezik, az alkalmazás olvasatlan üzenet értesítést jelenít meg.

Az üzeneteket a rendszer ellenőrzi a tiszteletteljes kommunikáció érdekében. Ha egy üzenet sérti a szabályokat, az alkalmazás figyelmeztetést jelenít meg, és az üzenetet nem fogadja el.

Stockfish ellen a chat nem érhető el.

### 3.11 Meccsopciók

A meccsopciók menüben a játszmához kapcsolódó műveletek érhetők el.

A meccs állapotától függően a fő művelet lehet:

- **Abort / Megszakítás**: a játszma legelején érhető el, mielőtt a játék ténylegesen előrehaladna.
- **Resign / Feladás**: akkor érhető el, amikor a játék már előrehaladt.
- **Quit to menu / Vissza a menübe**: a meccs befejezése után érhető el.

További műveletek:

- **Offer Draw / Döntetlen ajánlása**: döntetlenajánlat küldése az ellenfélnek.
- **Settings / Beállítások**: játék közbeni beállítások megnyitása.
- **Go back / Vissza**: opciómenü bezárása.

Stockfish ellen döntetlenajánlat nem jelenik meg.

### 3.12 Döntetlenajánlatok

Döntetlen ajánlása esetén:

- az ellenfél megerősítő kérdést kap
- az ellenfél elfogadhatja vagy elutasíthatja az ajánlatot
- elfogadás esetén a meccs közös megegyezéssel döntetlennel zárul

Ha te ajánlasz döntetlent, az alkalmazás a meccs oldalsávján megjeleníti a megfelelő állapotot.

### 3.13 Feladás

A **Resign / Feladás** művelet a meccsopciók menüből választható.

Az alkalmazás megerősítést kér a feladás elküldése előtt. Ha a felhasználó megerősíti, a meccs véget ér, és az ellenfél feladás miatt nyer.

### 3.14 Meccseredmények

A meccs végén az alkalmazás eredményképernyőt jelenít meg.

Lehetséges eredmények:

- győztes meccs
- elveszített meccs
- döntetlen meccs
- megszakított játszma

Lehetséges befejezési okok:

- sakkmatt
- időtúllépés
- feladás
- közös megegyezés
- patt
- 50 egymást követő lépés
- 75 egymást követő lépés
- elégtelen anyag
- háromszori állásismétlés
- ötszöri állásismétlés

Az eredményképernyőn elérhető műveletek:

- **Play again / Új játék**
- **Quit to menu / Vissza a menübe**
- játékos profiljának megnyitása
- ellenfél profiljának megnyitása, kivéve Stockfish esetén

### 3.15 Alkalmazásbeállítások

Az asztali alkalmazás beállításai:

- **Board Theme / Táblatéma**
  - Wooden
  - Modern
  - Royal
- **Piece Theme / Bábutéma**
  - Default
- **Language / Nyelv**
  - System
  - English
  - Hungarian
- **Dark Mode / Sötét mód**
- **Disable Sounds / Hangok kikapcsolása**
- **Auto-Queen / Automatikus vezér**

A beállítások automatikusan mentésre kerülnek, és az alkalmazás következő indításakor is megmaradnak.

### 3.16 Hangok

Az asztali alkalmazás hangokat használ különböző sakkeseményekhez, például:

- meccs kezdete
- lépés
- ütés
- sáncolás
- sakk
- átváltozás
- szabálytalan lépés
- meccs vége

A **Disable Sounds / Hangok kikapcsolása** beállítással a hangok elnémíthatók.

## 4. Felhasználási feltételek és fair play

A NeuChessHu fair playre, gyakorlásra, tanulásra és szórakozásra szolgál.

A felhasználók nem tehetik meg az alábbiakat:

- csalás
- sakkmotor használata más játékos elleni meccs közben
- bot vagy automatizált segítség használata
- külső segítség igénybevétele játék közben
- hibák kihasználása
- eredmények manipulálása
- mások zaklatása vagy fenyegetése
- gyűlöletbeszéd, diszkrimináció, spam vagy sértő viselkedés

A chatüzeneteknek tiszteletteljesnek kell maradniuk. Az alkalmazás elutasíthatja azokat az üzeneteket, amelyek sértik a kommunikációs szabályokat.

## 5. Hibaelhárítás

### Nem sikerül bejelentkezni

Ellenőrizd, hogy:

- helyes-e az e-mail cím
- helyes-e a jelszó
- létezik-e a fiók

Ha a weboldal érvénytelen hitelesítési adatokat jelez, akkor az e-mail címet vagy a jelszót nem fogadta el.

### Nem lehet továbblépni a regisztrációban

Ellenőrizd a kijelölt mezőket. Gyakori okok:

- hibás e-mail formátum
- túl rövid jelszó
- a jelszó nem tartalmaz betűt és számot is
- a jelszó megerősítése nem egyezik
- hiányzó becenév
- érvénytelen születési dátum
- a felhasználási feltételek nincsenek elfogadva

### A becenevet nem fogadja el a rendszer

Lehetséges, hogy a becenév már foglalt. Válassz másik becenevet, és próbáld újra.

### Nem működik a profilkép feltöltése

Ellenőrizd, hogy:

- a fájl képformátumú-e
- a kép feldolgozás után sem túl nagy-e
- a böngésző hozzáférhet-e a kiválasztott fájlhoz

### Nem indul el a meccs az asztali alkalmazásban

Ellenőrizd, hogy:

- be vagy-e jelentkezve
- van-e kiválasztott érvényes időkontroll

### Hiányzik a chat

A chat Stockfish ellen le van tiltva. Csak játékosok közötti meccseken érhető el.

### Hiányzik a döntetlenajánlat

A döntetlenajánlat Stockfish ellen le van tiltva. Játékosok közötti meccsen a meccsopciók menüből érhető el.

### Az asztali alkalmazás böngészőt nyit bejelentkezéshez

Ez elvárt működés. Az asztali alkalmazás a weboldal bejelentkezési folyamatát használja, majd sikeres hitelesítés után callbacken keresztül visszatér az alkalmazásba.

## 6. Gyors kezdés

1. Nyisd meg a NeuChessHu weboldalt.
2. Hozz létre fiókot a háromlépéses regisztrációval.
3. Töltsd le a Windows asztali alkalmazást.
4. Nyisd meg az asztali alkalmazást.
5. Jelentkezz be, amikor az alkalmazás kéri.
6. Válassz időkontrollt.
7. Indíts párkeresést, vagy válaszd a **Play against Stockfish / Játék Stockfish ellen** lehetőséget.
8. Játszd le a meccset.
9. Nézd meg az eredményt és a meccsstatisztikákat a profilodon.
