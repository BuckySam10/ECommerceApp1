using ECommerceApp.DTOs.User;
using System.Security.Claims;

namespace ECommerceApp.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string Token, UserResponseDto User)> LoginAsync(LoginRequestDto loginRequest);
        Task<(bool Success, string Message, UserResponseDto User)> RegisterAsync(RegisterRequestDto registerRequest);
        Task<UserResponseDto?> GetUserByIdAsync(int userId);
        ClaimsPrincipal? ValidateToken(string token);
        int? GetUserIdFromToken(string token);
    }
}
