# NeuChess Backend - Tesztelési Dokumentáció

## Összefoglaló

- Tesztesetek száma: 105
- Sikeres tesztek: 105

## Tesztek lényege

- **TC-001** (–)
  A teszt azt ellenőrzi, hogy GET /api/users – lista lekérés. A várt kimenet: sikeres futás és helyes viselkedés.

- **TC-002** (–)
  A teszt azt ellenőrzi, hogy POST /api/users – új felhasználó létrehozás. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-003** (–)
  A teszt azt ellenőrzi, hogy GET /api/matches – meccs lista. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-004** (–)
  A teszt azt ellenőrzi, hogy GET /api/tournaments – torna lista. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-005** (–)
  A teszt azt ellenőrzi, hogy web socket test: ws://backend.vm2.test:6001/app/050fddd54b732a7d4754?protocol=7&client=js&version=8.4.0&flash=false. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-006** (–)
  A teszt azt ellenőrzi, hogy web socket test: ws://backend.vm2.test:7001. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-007** (testDiagonalMoveOneStepIsLegal)
  A teszt azt ellenőrzi, hogy átlós lépés (1 mező) – [4,4]→[5,5], [3,3], [5,3], [3,5]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-008** (testDiagonalMoveMultipleStepsIsLegal)
  A teszt azt ellenőrzi, hogy átlós lépés (több mező) – [0,0]→[7,7], [7,0]→[0,7]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-009** (testHorizontalMoveIsIllegal)
  A teszt azt ellenőrzi, hogy vízszintes lépés tiltva – [4,4]→[4,6]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-010** (testVerticalMoveIsIllegal)
  A teszt azt ellenőrzi, hogy függőleges lépés tiltva – [4,4]→[6,4]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-011** (testKnightShapedMoveIsIllegal)
  A teszt azt ellenőrzi, hogy l-alakú lépés tiltva – [4,4]→[6,5]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-012** (testTwoRowsOneColumnMovesAreLegal)
  A teszt azt ellenőrzi, hogy 2 sor + 1 oszlop kombinációk érvényesek – [4,4]→[2,5],[2,3],[6,5],[6,3]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-013** (testOneRowTwoColumnMovesAreLegal)
  A teszt azt ellenőrzi, hogy 1 sor + 2 oszlop kombinációk érvényesek – [4,4]→[3,6],[5,6],[3,2],[5,2]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-014** (testDiagonalMoveIsIllegal)
  A teszt azt ellenőrzi, hogy átlós lépés tiltva – [4,4]→[5,5]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-015** (testStraightMovesAreIllegal)
  A teszt azt ellenőrzi, hogy vízszintes/függőleges lépés tiltva. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-016** (testTwoByTwoMoveIsIllegal)
  A teszt azt ellenőrzi, hogy 2×2 lépés tiltva – [4,4]→[2,2]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-017** (testThreeOneMoveIsIllegal)
  A teszt azt ellenőrzi, hogy 3+1 kombinált lépés tiltva – [4,4]→[7,5]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-018** (testHorizontalMoveIsLegal)
  A teszt azt ellenőrzi, hogy vízszintes lépés érvényes – [4,0]→[4,7]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-019** (testVerticalMoveIsLegal)
  A teszt azt ellenőrzi, hogy függőleges lépés érvényes – [0,4]→[7,4]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-020** (testDiagonalMoveIsIllegal)
  A teszt azt ellenőrzi, hogy átlós lépés tiltva – [4,4]→[5,5]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-021** (testKnightShapedMoveIsIllegal)
  A teszt azt ellenőrzi, hogy l-alakú lépés tiltva – [4,4]→[6,5]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-022** (testHorizontalMoveIsLegal)
  A teszt azt ellenőrzi, hogy vízszintes lépés érvényes – [4,0]→[4,7]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-023** (testVerticalMoveIsLegal)
  A teszt azt ellenőrzi, hogy függőleges lépés érvényes – [0,4]→[7,4]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-024** (testDiagonalMoveIsLegal)
  A teszt azt ellenőrzi, hogy átlós lépés érvényes – [0,0]→[7,7]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-025** (testKnightShapedMoveIsIllegal)
  A teszt azt ellenőrzi, hogy l-alakú lépés tiltva – [4,4]→[6,5]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-026** (testBlackPawnSingleStepForwardIsLegal)
  A teszt azt ellenőrzi, hogy fekete – egy lépés előre (lefelé) érvényes: [3,3]→[4,3]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-027** (testBlackPawnDoubleStepFromStartingRowIsLegal)
  A teszt azt ellenőrzi, hogy fekete – két lépés előre alapsorból (1. sor): [1,3]→[3,3]. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-028** (testBlackPawnBackwardMoveIsIllegal)
  A teszt azt ellenőrzi, hogy fekete – hátra lépés (felfelé) tiltva. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-029** (testBlackPawnDiagonalCaptureIsLegal)
  A teszt azt ellenőrzi, hogy fekete – átlós ütés érvényes. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-030** (testKingsOnlyIsDraw)
  A teszt azt ellenőrzi, hogy üres tábla (csak királyok) → döntetlen. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-031** (testOneKnightIsDraw)
  A teszt azt ellenőrzi, hogy egy huszár a táblán → döntetlen. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-032** (testOneBishopIsDraw)
  A teszt azt ellenőrzi, hogy egy futó a táblán → döntetlen. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-033** (testTwoBishopsOnSameColoredSquaresIsDraw)
  A teszt azt ellenőrzi, hogy két futó azonos mező-színen → döntetlen. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-034** (testTwoBishopsOnDifferentColoredSquaresIsNotDraw)
  A teszt azt ellenőrzi, hogy két futó különböző mező-színen → nem döntetlen. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-035** (testPawnOnBoardIsNotDraw)
  A teszt azt ellenőrzi, hogy gyalog a táblán → nem döntetlen. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-036** (testRookOnBoardIsNotDraw)
  A teszt azt ellenőrzi, hogy bástya a táblán → nem döntetlen. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-037** (testQueenOnBoardIsNotDraw)
  A teszt azt ellenőrzi, hogy királynő a táblán → nem döntetlen. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-038** (testTwoBishopsSameSideSameSquareColorIsNotDraw)
  A teszt azt ellenőrzi, hogy két azonos oldali futó azonos mező-színen → nem döntetlen. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-039** (testValidPngPasses)
  A teszt azt ellenőrzi, hogy érvényes 1×1 PNG base64 → nincs hiba. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-040** (testMissingPrefixFails)
  A teszt azt ellenőrzi, hogy hiányzó data URI prefix → JPEG or PNG hiba. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-041** (testGifPrefixFails)
  A teszt azt ellenőrzi, hogy gIF prefix → JPEG or PNG hiba. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-042** (testInvalidBase64Fails)
  A teszt azt ellenőrzi, hogy érvénytelen base64 tartalom → hiba. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-043** (testOversizedImageFails)
  A teszt azt ellenőrzi, hogy 2 MB feletti méret → 2MB hiba. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-044** (testGetMatchFromCacheReturnsExistingData)
  A teszt azt ellenőrzi, hogy getMatchFromCache – meglévő adat visszaadása. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-045** (testGetMatchFromCacheReturnsNullWhenMissing)
  A teszt azt ellenőrzi, hogy getMatchFromCache – null ha nincs adat. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-046** (testUpdateMatchInCacheStoresUpdatedData)
  A teszt azt ellenőrzi, hogy updateMatchInCache – frissített adat tárolva. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-047** (testRemoveMatchFromCacheDeletesEntry)
  A teszt azt ellenőrzi, hogy removeMatchFromCache – true + adat törölve. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-048** (testRemoveMatchFromCacheOnNonexistentKeyDoesNotThrow)
  A teszt azt ellenőrzi, hogy removeMatchFromCache – nem létező kulcsnál nem dob hibát. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-049** (testBoardHas8Rows)
  A teszt azt ellenőrzi, hogy tábla mérete: 8 sor. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-050** (testBoardHas8ColumnsPerRow)
  A teszt azt ellenőrzi, hogy tábla mérete: 8 oszlop/sor. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-051** (testBoardHasNoNullCells)
  A teszt azt ellenőrzi, hogy nincs null cella a táblán. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-052** (testBlackBackRankOrder)
  A teszt azt ellenőrzi, hogy fekete hátsó sor sorrendje: R,N,B,Q,K,B,N,R. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-053** (testBlackPawnsOnRow1)
  A teszt azt ellenőrzi, hogy fekete gyalogsor (1. sor): 8 Pawn(Black). A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-054** (testWhitePawnsOnRow6)
  A teszt azt ellenőrzi, hogy fehér gyalogsor (6. sor): 8 Pawn(White). A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-055** (testWhiteBackRankOrder)
  A teszt azt ellenőrzi, hogy fehér hátsó sor sorrendje: R,N,B,Q,K,B,N,R. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-056** (testMiddleRowsAreEmptyPieces)
  A teszt azt ellenőrzi, hogy közép sorok üresek (2–5): Piece::None / Side::None. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-057** (testTotalNonEmptyPiecesIs32)
  A teszt azt ellenőrzi, hogy összes nem üres bábu száma: 32. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-058** (testEachSideHas16Pieces)
  A teszt azt ellenőrzi, hogy mindkét oldal: 16 bábu. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-059** (testEachSideHas8Pawns)
  A teszt azt ellenőrzi, hogy mindkét oldal: 8 gyalog. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-060** (testEachSideHas2Rooks)
  A teszt azt ellenőrzi, hogy mindkét oldal: 2 bástya. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-061** (testEachSideHas1King)
  A teszt azt ellenőrzi, hogy mindkét oldal: 1 király. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-062** (testCreateDataStoreSetsMatchIdWithSanitizedDurationAndChannel)
  A teszt azt ellenőrzi, hogy createDataStore – MatchID sanitált időtartamot tartalmaz: '3|2-lobby-1'. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-063** (testCreateDataStoreRemovesSpacesFromDurationInMatchId)
  A teszt azt ellenőrzi, hogy createDataStore – Szóközök eltávolítása duration-ből: '10|0-room'. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-064** (testCreateDataStorePlayedAtIsRecent)
  A teszt azt ellenőrzi, hogy createDataStore – PlayedAt aktuális időpont. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-065** (testCreateDataStoreInitializesEmptyChatMessages)
  A teszt azt ellenőrzi, hogy createDataStore – Üres ChatMessages inicializálás. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-066** (testCreateDataStoreSetsMatchDurationOnMatchState)
  A teszt azt ellenőrzi, hogy createDataStore – MatchDuration beállítva MatchState-en. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-067** (testCreateDataStoreCreatesTwoPlayers)
  A teszt azt ellenőrzi, hogy createDataStore – Két játékos létrehozva, ID-k helyesek. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-068** (testCreateDataStoreClocksMatchDuration)
  A teszt azt ellenőrzi, hogy createDataStore – Óra alap ms: WhiteRemainingMs=180000. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-069** (testPawnSimpleMove)
  A teszt azt ellenőrzi, hogy gyalog egyszerű lépés → 'e5'. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-070** (testPawnCaptureIncludesFromColumn)
  A teszt azt ellenőrzi, hogy gyalog ütés (from oszlop megjelenik) → 'exf5'. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-071** (testKnightMoveNotation)
  A teszt azt ellenőrzi, hogy huszár lépés notáció → 'Nf3'. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-072** (testBishopMoveNotation)
  A teszt azt ellenőrzi, hogy futó lépés notáció → 'Be5'. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-073** (testRookMoveNotation)
  A teszt azt ellenőrzi, hogy bástya lépés notáció → 'Re1'. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-074** (testQueenMoveNotation)
  A teszt azt ellenőrzi, hogy vezér lépés notáció → 'Qd4'. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-075** (testKingMoveNotation)
  A teszt azt ellenőrzi, hogy király lépés notáció → 'Ke5'. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-076** (testDesktopAuthorizeRedirectsGuestsToSignIn)
  A teszt azt ellenőrzi, hogy desktop authorize guest esetén átirányítás a sign in oldalra (redirect_uri megtartásával). A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-077** (testRandomDirectionIsIllegal)
  A teszt azt ellenőrzi, hogy érvénytelen, véletlenszerű vezérirány tiltva – szabálytalan lépés elutasítása.. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-078** (testRookCaptureNotation)
  A teszt azt ellenőrzi, hogy bástya ütés notáció → 'Rxe1'. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-079** (testValidMessagePasses)
  A teszt azt ellenőrzi, hogy érvényes üzenet – nincs hiba: 'Hello, good game!'. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-080** (testSingleCharacterPasses)
  A teszt azt ellenőrzi, hogy egy betűs üzenet érvényes: 'a'. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-081** (testNonStringFails)
  A teszt azt ellenőrzi, hogy nem string érték → hibát dob (42). A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-082** (testEmptyStringFails)
  A teszt azt ellenőrzi, hogy üres string → hibát dob. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-083** (testWhitespaceOnlyFails)
  A teszt azt ellenőrzi, hogy csak szóközök → hibát dob (trim után üres). A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-084** (test101CharactersFails)
  A teszt azt ellenőrzi, hogy 101 karakter → hibát dob (határérték). A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-085** (testEnglishBannedWordFails)
  A teszt azt ellenőrzi, hogy angol tiltott szó 'fuck' → inappropriate hiba. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-086** (testHungarianBannedWordFails)
  A teszt azt ellenőrzi, hogy magyar tiltott szó 'kurva' → inappropriate hiba. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-087** (testLeetSpeakBannedWordFails)
  A teszt azt ellenőrzi, hogy leetspeak tiltott szó 'f4ggot' → hiba. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-088** (testPatternBasedBannedWordFails)
  A teszt azt ellenőrzi, hogy regex-minta 'f*ck' → inappropriate hiba. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-089** (testExcessiveCapitalizationFails)
  A teszt azt ellenőrzi, hogy túlzott nagybetűsítés >70% → capitalization hiba. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-090** (testNormalCapitalizationPasses)
  A teszt azt ellenőrzi, hogy normális nagybetűsítés → nincs hiba. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-091** (testRepeatedCharactersFail)
  A teszt azt ellenőrzi, hogy 7+ ismétlődő karakter → repeated hiba. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-092** (testSixRepeatedCharactersPasses)
  A teszt azt ellenőrzi, hogy 6 ismétlődő karakter (határérték) → nincs hiba. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-093** (testNoLettersFails)
  A teszt azt ellenőrzi, hogy csak számok/szimbólumok betű nélkül → readable hiba. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-094** (testMessageWithNumbersAndLetterPasses)
  A teszt azt ellenőrzi, hogy szám + legalább egy betű → érvényes. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-095** (testValidBase64ButNotImageFails)
  A teszt azt ellenőrzi, hogy érvényes base64, de nem kép tartalom → hiba. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-096** (testJpegPrefixIsAcceptedFormat)
  A teszt azt ellenőrzi, hogy jPEG prefix elfogadott – JPEG or PNG hiba nem jelenik meg. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-097** (testValidMessageReturnsSuccessStatus)
  A teszt azt ellenőrzi, hogy érvényes üzenet → Status='Success'. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-098** (testValidMessageReturnsNewMessageData)
  A teszt azt ellenőrzi, hogy érvényes üzenet → NewMessage tartalmazza a UserID-t és üzenetet. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-099** (testValidMessageIsAppendedToChatMessages)
  A teszt azt ellenőrzi, hogy két érvényes üzenet felhalmozódik (count=2, sorrend helyes). A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-100** (testValidMessageStoresCorrectUserId)
  A teszt azt ellenőrzi, hogy helyes UserID tárolása a ChatMessages-ben. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-101** (testSingleCharacterMessageIsValid)
  A teszt azt ellenőrzi, hogy 1 karakteres üzenet érvényes → Success. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-102** (testMessageExceeding100CharsReturnsViolation)
  A teszt azt ellenőrzi, hogy 101 karakteres üzenet → Status='Violation'. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-103** (testBannedEnglishWordReturnsViolation)
  A teszt azt ellenőrzi, hogy tiltott angol szó → Status='Violation'. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-104** (testBannedHungarianWordReturnsViolation)
  A teszt azt ellenőrzi, hogy tiltott magyar szó → Status='Violation'. A várt kimenet: sikeres futás és helyes viselkedés.
- **TC-105** (testMultipleValidMessagesAccumulate)
  A teszt azt ellenőrzi, hogy 5 érvényes üzenet sorban → count=5. A várt kimenet: sikeres futás és helyes viselkedés.
