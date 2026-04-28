using System.Windows;

namespace NeuChessHu.Resources.Languages;

internal class EnglishDictionary
{
    readonly static ResourceDictionary resources = Application.Current.Resources;

    internal static void SetLanguage()
    {
        SetCommonTexts();
        SetMenuSideBarTexts();
        SetMatchSideBarTexts();
        SetSettingsNames();
        SetOptionsPopUpTexts();
        SetLookingForMatchWindowTexts();
        SetSettingsValues();
        SetMatchEndWindowTexts();
        SetMenuPopUpTexts();
        SetLoginPopUpTexts();
        SetTimeSetterWindowTexts();
    }

    static void SetCommonTexts()
    {
        resources["GoBackText"] = "Go back";
        resources["SettingsText"] = "Settings";
    }

    static void SetMenuSideBarTexts()
    {
        resources["StartGameText"] = "Start Game";
        resources["MoreOptionsText"] = "More";
        resources["PlayStockfishText"] = "Play against Stockfish";
    }

    static void SetMatchSideBarTexts()
    {
        resources["ChatInputPlaceHolderText"] = "Message...";
        resources["ViolationNotificationText"] = "Please, communicate with respect!";
        resources["ResignConfirmationText"] = "Resign?";
        resources["DrawConfirmationText"] = "Draw?";
    }

    static void SetSettingsNames()
    {
        resources["BoardThemeText"] = "Board Theme";
        resources["PieceThemeText"] = "Piece Theme";
        resources["LanguageText"] = "Language";
        resources["DarkModeText"] = "Dark Mode";
        resources["DisableSoundsText"] = "Disable Sounds";
        resources["AutoQueenText"] = "Auto-Queen";
    }

    static void SetSettingsValues()
    {
        resources["DarkAppTheme"] = "Dark";
        resources["LightAppTheme"] = "Light";

        resources["ModernBoardTheme"] = "Modern Style";
        resources["PastelGreenBoardTheme"] = "Pastel Green";
        resources["RoyalBoardTheme"] = "Royal";
        resources["WoodenBoardTheme"] = "Wooden Style";

        resources["SystemLanguage"] = "System";
        resources["EnglishLanguage"] = "English";
        resources["HungarianLanguage"] = "Hungarian";
    }

    static void SetOptionsPopUpTexts()
    {
        resources["AbortText"] = "Abort";
        resources["ResignText"] = "Resign";
        resources["DrawText"] = "Offer Draw";
    }

    static void SetLookingForMatchWindowTexts()
    {
        resources["LookingForMatchText"] = "Looking for Match";
        resources["AgainstStockfishText"] = "Against Stockfish";
        resources["SearchingNotes"] = new List<string>
        {
            "Waiting for a worthy opponent...",
            "Scanning the horizon for \na challenger...",
            "Searching for someone \nbrave enough to face you...",
            "Preparing the battlefield \nfor your next rival...",
            "Looking for an opponent who \nwon’t resign instantly...",
            "Seeking a mind equal \nto yours...",
            "Your next victim \nis being selected...",
            "Finding a player who knows \nwhat they’re getting into...",
            "The algorithm whispers: \na challenger approaches",
            "Your adversary is almost ready",
            "Searching for opponent…",
            "Hunting for a challenger…",
            "Scanning for rival…",
            "Looking for a brave opponent…",
            "Preparing the battlefield…",
            "A challenger approaches…",
            "Loading opponent…",
            "Seeking your next target…",
            "Selecting an opponent…",
            "Opponent incoming…",
            "Almost found your rival…",
            "Matchmaking in progress…"
        };
    }

    static void SetMatchEndWindowTexts()
    {
        resources["MatchResultLostText"] = "Match Lost!";
        resources["MatchResultDrawnText"] = "Match Drawn!";
        resources["MatchResultWonText"] = "Match Won!";
        resources["MatchAbortedText"] = "Game Aborted!";

        resources["CheckmateText"] = "By Checkmate";
        resources["TimedoutText"] = "By Timeout";
        resources["ResignationText"] = "By Resignation";

        resources["MutualAgreementText"] = "By Mutual Agreement";
        resources["StalemateText"] = "By Stalemate";
        resources["FiftyConsecutiveMovesTexts"] = "By 50 Consecutive Moves";
        resources["SeventyFiveConsecutiveMovesTexts"] = "By 75 Consecutive Moves";
        resources["InsufficentMaterialText"] = "By Insufficent Materials";
        resources["ThreefoldRepetition"] = "By Threefold-Repetition";
        resources["FivefoldRepetition"] = "By Fivefold-Repetition";

        resources["PlayAgainButtonText"] = "Play again";
        resources["QuitToMenuButtonText"] = "Quit to menu";
    }

    static void SetMenuPopUpTexts()
    {
        resources["ProfileSettingsText"] = "Profile Settings";
        resources["LogoutText"] = "Logout";
        resources["QuitApplicationText"] = "Quit";
    }

    static void SetLoginPopUpTexts()
    {
        resources["LoginText"] = "Login";
        resources["LoginNotificationText"] = "Please, log in to access this feature!";
    }

    static void SetTimeSetterWindowTexts()
    {
        resources["ChooseTimeText"] = "Choose Time";
        resources["BulletText"] = "Bullet";
        resources["BlitzText"] = "Blitz";
        resources["RapidText"] = "Rapid";
        resources["BulletOneMinText"] = "1 min";
        resources["BlitzThreeMinText"] = "3 min";
        resources["BlitzFiveMinText"] = "5 min";
        resources["RapidTenMinText"] = "10 min";
        resources["RapidThirtyMinText"] = "15 min";
        resources["MinuteText"] = "min";
    }
}