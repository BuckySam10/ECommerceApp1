namespace ECommerceApp.Services.Interfaces
{
    public interface IHashedPassword
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
