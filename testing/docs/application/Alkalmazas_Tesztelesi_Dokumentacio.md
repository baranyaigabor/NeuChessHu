# NeuChess Asztali Alkalmazás - Tesztelési Dokumentáció

## Összefoglaló

- Tesztesetek száma: 43
- Sikeres tesztek: 43

## Tesztek lényege

- **TC-001** (ExtractLoginDetailsWithValidEncodedDataReturnsTokenAndUserId)
  A teszt azt ellenőrzi, hogy érvényes kódolt adatból 'token' és 'userId' helyes kinyerése. A várt kimenet: sikeres.

- **TC-002** (ExtractLoginDetailsWhenDataIsMissingThrows)
  A teszt azt ellenőrzi, hogy hiányzó adat esetén kivételt dob. A várt kimenet: sikeres.

- **TC-003** (UserWhenSetRaisesPropertyChanged)
  A teszt azt ellenőrzi, hogy user beállításakor PropertyChanged esemény kiváltódik. A várt kimenet: sikeres.

- **TC-004** (ClearSessionWithExistingValuesRemovesTokenUserAndUserId)
  A teszt azt ellenőrzi, hogy session törléskor 'token', user és userId eltávolítva. A várt kimenet: sikeres.

- **TC-005** (LogoutAsyncWhenTokenIsMissingDoesNotClearCurrentSession)
  A teszt azt ellenőrzi, hogy kijelentkezésnél hiányzó 'token' esetén a session nem törlődik. A várt kimenet: sikeres.

- **TC-006** (BoardFillerWhenPlayingWhiteCreatesStandardWhitePerspectiveBoard)
  A teszt azt ellenőrzi, hogy fehér nézetből a tábla a standard kezdőállást generálja. A várt kimenet: sikeres.

- **TC-007** (BoardFillerWhenPlayingBlackFlipsBackRankAndPawnRows)
  A teszt azt ellenőrzi, hogy fekete nézetből a hátsó sor és gyalogsor megfordul. A várt kimenet: sikeres.

- **TC-008** (CreateWithRealPieceReturnsPieceAndSide)
  A teszt azt ellenőrzi, hogy valós bábu létrehozásakor Piece és Side helyes értéket ad vissza. A várt kimenet: sikeres.

- **TC-009** (CreateWithNonePieceReturnsEmptySquare)
  A teszt azt ellenőrzi, hogy none bábuval üres mező jön létre. A várt kimenet: sikeres.

- **TC-010** (RaisePropertyChangedWithoutNameUsesCallerMemberName)
  A teszt azt ellenőrzi, hogy név nélküli PropertyChanged a hívó tag nevét használja. A várt kimenet: sikeres.

- **TC-011** (RaisePropertyChangedWithExplicitNameUsesProvidedName)
  A teszt azt ellenőrzi, hogy explicit névvel a megadott property névvel váltódik ki az esemény. A várt kimenet: sikeres.

- **TC-012** (StartWithShortTimeUsesSubsecondTicking)
  A teszt azt ellenőrzi, hogy rövid idő esetén az óra ezredmásodperces ütemezéssel ketyeg. A várt kimenet: sikeres.

- **TC-013** (StopWhenTimerIsRunningPreventsFurtherTicks)
  A teszt azt ellenőrzi, hogy stop meghívásakor a futó időzítő leáll, nem ketyeg tovább. A várt kimenet: sikeres.

- **TC-014** (TimeFormatterWithMidGameSecondsReturnsWholeSeconds)
  A teszt azt ellenőrzi, hogy játék közbeni másodpercek egész másodpercként jelennek meg. A várt kimenet: sikeres.

- **TC-015** (ReadJsonWithPieceAndSideNameReturnsChessPiece)
  A teszt azt ellenőrzi, hogy jSON-ból Piece és Side névvel ChessPiece objektum visszaolvasható. A várt kimenet: sikeres.

- **TC-016** (ReadJsonWithInvalidValueThrowsJsonSerializationException)
  A teszt azt ellenőrzi, hogy érvénytelen JSON érték JsonSerializationException-t dob. A várt kimenet: sikeres.

- **TC-017** (WriteJsonWithPieceWritesPieceAndSideName)
  A teszt azt ellenőrzi, hogy chessPiece JSON-ba íráskor Piece és Side neve kerül a kimenetbe. A várt kimenet: sikeres.

- **TC-018** (ReadJsonWithPieceNamesAndEmptyCellsReturnsMatrix)
  A teszt azt ellenőrzi, hogy bábu neveket és üres cellákat tartalmazó JSON mátrixot ad vissza. A várt kimenet: sikeres.

- **TC-019** (WriteJsonWithEightByEightMatrixWritesNestedPieceArrays)
  A teszt azt ellenőrzi, hogy 8×8-as mátrix JSON-ba íráskor egymásba ágyazott tömböket ír. A várt kimenet: sikeres.

- **TC-020** (ReadJsonWithEmptyStringReturnsNone)
  A teszt azt ellenőrzi, hogy üres JSON string esetén Piece.None értéket ad vissza. A várt kimenet: sikeres.

- **TC-021** (WriteJsonWithPieceWritesPieceName)
  A teszt azt ellenőrzi, hogy piece JSON-ba íráskor a bábu nevét írja ki. A várt kimenet: sikeres.

- **TC-022** (ReadJsonWithEmptyStringReturnsNull)
  A teszt azt ellenőrzi, hogy üres JSON string esetén null értéket ad vissza. A várt kimenet: sikeres.

- **TC-023** (WriteJsonWithSideWritesSideName)
  A teszt azt ellenőrzi, hogy side JSON-ba íráskor az oldal nevét írja ki. A várt kimenet: sikeres.

- **TC-024** (ReadJsonWithTwoItemArrayReturnsTuple)
  A teszt azt ellenőrzi, hogy két elemű JSON tömbből Tuple visszaolvasható. A várt kimenet: sikeres.

- **TC-025** (ReadJsonWithNullReturnsNull)
  A teszt azt ellenőrzi, hogy null JSON értékből null tuple jön vissza. A várt kimenet: sikeres.

- **TC-026** (WriteJsonWithTupleWritesTwoItemArray)
  A teszt azt ellenőrzi, hogy tuple JSON-ba íráskor két elemű tömb kerül a kimenetbe. A várt kimenet: sikeres.

- **TC-027** (ConstructorWithValuesStoresChannelAndPlayerID)
  A teszt azt ellenőrzi, hogy konstruktor a Channel és PlayerID értékeket helyesen tárolja. A várt kimenet: sikeres.

- **TC-028** (ConstructorWithValuesStoresInitializerFields)
  A teszt azt ellenőrzi, hogy konstruktor az összes inicializáló mezőt helyesen tárolja. A várt kimenet: sikeres.

- **TC-029** (ConstructorCreatesEmptyMatchModelsAndPlayerStores)
  A teszt azt ellenőrzi, hogy konstruktor üres meccs modelleket és játékos tárakat hoz létre. A várt kimenet: sikeres.

- **TC-030** (MatchPointsReasonWhenPatchedRaisesPropertyChanged)
  A teszt azt ellenőrzi, hogy matchPoints Reason patch után PropertyChanged eseményt vált ki. A várt kimenet: sikeres.

- **TC-031** (WinnerIDWhenPatchedRaisesPropertyChanged)
  A teszt azt ellenőrzi, hogy winnerID patch után PropertyChanged eseményt vált ki. A várt kimenet: sikeres.

- **TC-032** (MatchDurationWhenPatchedRaisesPropertyChanged)
  A teszt azt ellenőrzi, hogy matchDuration patch után PropertyChanged eseményt vált ki. A várt kimenet: sikeres.

- **TC-033** (CurrentSideWhenPatchedRaisesPropertyChanged)
  A teszt azt ellenőrzi, hogy currentSide patch után PropertyChanged eseményt vált ki. A várt kimenet: sikeres.

- **TC-034** (PointsWhenPatchedRaisesPropertyChanged)
  A teszt azt ellenőrzi, hogy points patch után PropertyChanged eseményt vált ki. A várt kimenet: sikeres.

- **TC-035** (TimeWhenPatchedRaisesPropertyChanged)
  A teszt azt ellenőrzi, hogy time patch után PropertyChanged eseményt vált ki. A várt kimenet: sikeres.

- **TC-036** (PatchMatchStateWithValuesUpdatesScalarStateAndFlipsBlackMatrix)
  A teszt azt ellenőrzi, hogy matchState patch: skalár értékek frissülnek, fekete mátrix megfordul. A várt kimenet: sikeres.

- **TC-037** (PatchMatchStateWithLatestNotationReplacesSameRoundOrAddsNewRound)
  A teszt azt ellenőrzi, hogy legújabb notáció: azonos kör felülíródik, új kör hozzáadódik. A várt kimenet: sikeres.

- **TC-038** (PatchPlayerDatasWithScalarValuesUpdatesSelectedPlayer)
  A teszt azt ellenőrzi, hogy játékos adat patch: skalár értékek a kiválasztott játékosnál frissülnek. A várt kimenet: sikeres.

- **TC-039** (PatchPlayerDatasWithCapturedPiecesReplacesCapturedPieceCollection)
  A teszt azt ellenőrzi, hogy leütött bábuk patch: a gyűjtemény teljes cseréje megtörténik. A várt kimenet: sikeres.

- **TC-040** (PatchMatchPointsWithValuesUpdatesMatchPointState)
  A teszt azt ellenőrzi, hogy matchPoints patch: az állapot értékei helyesen frissülnek. A várt kimenet: sikeres.

- **TC-041** (PatchMatchPointsWhenMatchEndsRunsOnMatchEndCallback)
  A teszt azt ellenőrzi, hogy meccs vége esetén az OnMatchEnd callback lefut. A várt kimenet: sikeres.

- **TC-042** (PatchChatMessagesWithStatusUpdatesStatus)
  A teszt azt ellenőrzi, hogy chat üzenet patch: státusz frissítése megtörténik. A várt kimenet: sikeres.

- **TC-043** (PatchChatMessagesWithNewMessageAppendsMessage)
  A teszt azt ellenőrzi, hogy chat üzenet patch: új üzenet hozzáfűzése megtörténik. A várt kimenet: sikeres.
  