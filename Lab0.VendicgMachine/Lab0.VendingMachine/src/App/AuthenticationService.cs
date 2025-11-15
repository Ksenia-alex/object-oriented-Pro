namespace Lab0.VendingMachine.App;

public class AuthenticationService
{
    private const string AdministratorPassword = "123";

    public bool ValidatePassword(string password)
    {
        return password == AdministratorPassword;
    }
}