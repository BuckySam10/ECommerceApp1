using ECommerceApp.Models;
using System.Security.Claims;

namespace ECommerceApp.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        ClaimsPrincipal? ValidateToken(string token);
    }
}
