namespace MauiApp1;

internal class User
{
    public string Login { get; set; }
    public string Password { get; set; }

    internal User(string login, string password)
    {
        Login = login;
        Password = password;
    }
}


