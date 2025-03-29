public class SessionService
{
    public string? UserName { get; set; }
    public string? UserEmail { get; set; }
    public bool IsLoggedIn { get; set; } = false;

  

    public void Login(string userName, string userEmail)
    {
        UserName = userName;
        UserEmail = userEmail;
        IsLoggedIn = true;
    }

    public void Logout()
    {
        UserName = null;
        UserEmail = null;
        IsLoggedIn = false;
    }
    public bool IsUserLoggedIn()
    {
        return IsLoggedIn;
    }
    
}