using System.Windows;

namespace NeuChessHu.Resources.Languages;

internal class HungarianDictionary
{
    readonly static ResourceDictionary resources = Application.Current.Resources;
    internal static void SetLanguage()
    {
        SetCommonTexts();
        SetMenuSideBarTexts();
        SetSettingsNames();
        SetSettingsValues();
        SetOptionsPopUpTexts();
        SetLookingForMatchWindowTexts();
        SetMatchEndWindowTexts();
        SetMenuPopUpTexts();
        SetLoginPopUpTexts();
        SetTimeSetterWindowTexts();
    }

    static void SetCommonTexts()
    {
        resources["GoBackText"] = "Vissza";
        resources["SettingsText"] = "Beállítások";
    }

    static void SetMenuSideBarTexts()
    {
        resources["StartGameText"] = "Játék indítása";
        resources["MoreOptionsText"] = "Több";
        resources["JoinTournamentText"] = "Tornák";
        resources["PlayStockfishText"] = "Játssz Stockfish-sel";
        resources["CustomGameText"] = "Custom játék";
    }

    static void SetSettingsNames()
    {
        resources["AppThemeText"] = "App témája";
        resources["BoardThemeText"] = "Tábla témája";
        resources["PieceThemeText"] = "Bábuk témája";
        resources["LanguageText"] = "Nyelv";
        resources["DisableSoundsText"] = "Hangok kikapcsolása";
        resources["FocusModeText"] = "Fókusz mód";
        resources["AutoQueenText"] = "Automatikus királynő";
        resources["ConfirmEachMoveText"] = "Lépések megerősítése";
    }

    static void SetSettingsValues()
    {
        resources["SystemAppTheme"] = "Rendszer";
        resources["DarkAppTheme"] = "Sötét";
        resources["LightAppTheme"] = "Világos";

        resources["BrightBlueBoardTheme"] = "Világos kék";
        resources["DeathBoardTheme"] = "Kietlen";
        resources["FradiBoardTheme"] = "Fradi színek";
        resources["ModernBoardTheme"] = "Modern hatású";
        resources["PastelBlueBoardTheme"] = "Pasztel kék";
        resources["PastelGreenBoardTheme"] = "Pasztel zöld";
        resources["PinkWorldBoardTheme"] = "Pink mámor";
        resources["RoyalBoardTheme"] = "Királyi";
        resources["WoodenBoardTheme"] = "Fa hatású";

        resources["SystemLanguage"] = "Rendszer";
        resources["EnglishLanguage"] = "Angol";
        resources["HungarianLanguage"] = "Magyar";
    }

    static void SetOptionsPopUpTexts()
    {
        resources["AbortText"] = "Játék elvetése";
        resources["ResignText"] = "Feladás";
        resources["DrawText"] = "Döntetlen felajánlása";
    }

    static void SetLookingForMatchWindowTexts()
    {
        resources["LookingForMatchText"] = "Meccs keresése";
        resources["SearchingNotes"] = new List<string>
        {
            "Méltó ellenfélre várakozás",
            "A kihívók horizontjának \npásztázása",
            "Bátor jelentkezők kutatása...",
            "Tábla előkészítése a \nkövetkező riválisnak...",
            "Ellenfél keresése, aki \nnem adja fel azonnal...",
            "Egy hozzád méltó elme felkutatása...",
            "A következő áldozat kiválasztása \nfolyamatban...",
            "Az algoritmus suttogja: \nközeledik egy kihívó",
            "Kemény legények felkutatása...",
            "Az ellenfeled már \nmajdnem készen áll...",
            "Ellenfél keresése…",
            "Kihívó vadászata…",
            "Ellenfél beolvasása…",
            "Rivális felkutatása…",
            "Keresem a bátor ellenfelet…",
            "Csata előkészítése…",
            "Közeledik egy kihívó…",
            "Ellenfél betöltése…",
            "Keresem a következő áldozatot…",
            "Ellenfél összeválogatása…",
            "Kihívó érkezik…",
            "Mindjárt megvan az ellenfél…"
        };
    }

    static void SetMatchEndWindowTexts()
    {
        resources["MatchResultLostText"] = "Vesztettél!";
        resources["MatchResultDrawnText"] = "Döntetlen!";
        resources["MatchResultWonText"] = "Győztél!";
        resources["MatchAbortedText"] = "Meccs Elvetve!";

        resources["CheckmateText"] = "Sakkmatt által";
        resources["TimedoutText"] = "Lejárt az idő";
        resources["ResignationText"] = "Feladás végett";

        resources["MutualAgreementText"] = "Közös megegyezéssel";
        resources["StalemateText"] = "Patt helyzet";
        resources["FiftyConsecutiveMovesTexts"] = "Ötvenlépés szabály miatt";
        resources["SeventyFiveConsecutiveMovesTexts"] = "Hetvenötlépés szabály miatt";
        resources["InsufficentMaterialText"] = "Lehetetlen állás miatt";
        resources["ThreefoldRepetition"] = "Háromszori lépésismétlés miatt";
        resources["FivefoldRepetition"] = "Ötszöri lépésismétlés miatt";

        resources["PlayAgainButtonText"] = "Új játék keresése";
        resources["QuitToMenuButtonText"] = "Kilépés a menübe";
    }

    static void SetMenuPopUpTexts()
    {
        resources["ProfileSettingsText"] = "Profil beállítások";
        resources["LogoutText"] = "Kijelentkezés";
    }

    static void SetLoginPopUpTexts()
    {
        resources["LoginText"] = "Bejelentkezés";
        resources["LoginNotificationText"] = "Kérlek, a folytatáshoz jelentkezz be!";
    }

    static void SetTimeSetterWindowTexts()
    {
        resources["ChooseTimeText"] = "Válassz játékhosszt";
        resources["BulletText"] = "Bullet";
        resources["BlitzText"] = "Blitz";
        resources["RapidText"] = "Rapid"; 
        resources["BulletOneMinText"] = "1 perc";
        resources["BlitzThreeMinText"] = "3 perc";
        resources["BlitzFiveMinText"] = "5 perc";
        resources["RapidTenMinText"] = "10 perc";
        resources["RapidThirtyMinText"] = "15 perc";
        resources["MinuteText"] = "perc";
    }
}
