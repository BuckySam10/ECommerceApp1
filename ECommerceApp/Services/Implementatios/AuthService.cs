using ECommerceApp.DTOs.User;
using ECommerceApp.Models;
using ECommerceApp.Repositories.Interfaces;
using ECommerceApp.Services.Interfaces;

namespace ECommerceApp.Services
{
    public interface IAuthService
    {
        Task<(bool Success, string Token, UserResponseDto User)> LoginAsync(LoginRequestDto loginRequest);
        Task<(bool Success, string Message, UserResponseDto User)> RegisterAsync(RegisterRequestDto registerRequest);
        Task<UserResponseDto?> GetUserByIdAsync(int userId);
    }

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashedPassword _passwordService;
        private readonly IJwtService _jwtService;

        public AuthService(IUserRepository userRepository, IHashedPassword passwordService, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        public async Task<(bool Success, string Token, UserResponseDto User)> LoginAsync(LoginRequestDto loginRequest)
        {
            var user = await _userRepository.GetByUsernameAsync(loginRequest.Username);

            if (user == null || !user.IsActive)
            {
                return (false, string.Empty, null);
            }

            if (!_passwordService.VerifyPassword(loginRequest.Password, user.PasswordHash))
            {
                return (false, string.Empty, null);
            }

            var token = _jwtService.GenerateToken(user);
            var userResponse = MapToUserResponse(user);

            return (true, token, userResponse);
        }

        public async Task<(bool Success, string Message, UserResponseDto User)> RegisterAsync(RegisterRequestDto registerRequest)
        {
            // Check if username already exists
            var existingUser = await _userRepository.GetByUsernameAsync(registerRequest.Username);
            if (existingUser != null)
            {
                return (false, "Username already exists", null);
            }

            // Create new user
            var user = new User
            {
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Username = registerRequest.Username,
                PasswordHash = _passwordService.HashPassword(registerRequest.Password),
                Address = registerRequest.Address,
                Phone = registerRequest.Phone,
                Role = "User", // Default role
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _userRepository.AddUserAsync(user);
            var userResponse = MapToUserResponse(user);

            return (true, "User registered successfully", userResponse);
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            return user != null ? MapToUserResponse(user) : null;
        }

        private UserResponseDto MapToUserResponse(User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Address = user.Address,
                Phone = user.Phone,
                IsActive = user.IsActive,
                Role = user.Role
            };
        }
    }
}