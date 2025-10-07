namespace object_oriented_Pro.lab1.App;

public class AuthenticationService
{
    private const string AdministratorPassword = "123";

    public bool ValidatePassword(string password)
    {
        return password == AdministratorPassword;
    }
}