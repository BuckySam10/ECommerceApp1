namespace ECommerceApp.Services.Interfaces
{
    public interface IHashedPassword
    {
        string HashPassword(string password);
        bool ConfirmPassword(string password, string hashedPassword);
    }
}
