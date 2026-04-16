using ChessMechanics.Authentication.User;
using ChessMechanics.Common;

namespace ChessMechanics.Authentication.Session;

public class SessionDatas : ObservableBase
{
    UserData? user;

    public string? Token { get; internal set; }
    public int? UserID { get; internal set; }
    public UserData? User 
    { 
        get => user; 
        set
        {
            user = value;
            RaisePropertyChanged();
        }
    }

    internal void ClearSession()
    {
        Token = null;
        User = null;
        UserID = null;
    }
}