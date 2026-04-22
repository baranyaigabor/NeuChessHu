using ChessMechanics.Authentication.User;
using ChessMechanics.Common;

namespace ChessMechanics.Authentication.Session;

public class SessionDatas : ObservableBase
{
    UserData? user;

    public string? Token { get; set; }
    public int? UserID { get; set; }
    public UserData? User 
    { 
        get => user; 
        set
        {
            user = value;
            RaisePropertyChanged();
        }
    }

    public void ClearSession()
    {
        Token = null;
        User = null;
        UserID = null;
    }
}