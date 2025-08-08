using AlignTech.WebAPI.DataFirst.DTOs;
using AlignTech.WebAPI.DataFirst.Interfaces;
using AlignTech.WebAPI.DataFirst.Models;
using Microsoft.AspNetCore.Identity;

namespace AlignTech.WebAPI.DataFirst.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        public async Task<User> CreateUserAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                Name = registerDto.Name,
                Username = registerDto.Username
            };
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, registerDto.Password);
            user.PasswordHash = hashedPassword;

            var result = await _authRepository.RegisterAsync(user);
            return result ?? null!;
        }

        public async Task<string> LoginAsync(UserDto userDto)
        {
            var userData = await _authRepository.LoginAsync(userDto.Username);
            if (userData == null)
            {
                return null!;
            }

            if (new PasswordHasher<User>().VerifyHashedPassword(userData, userData.PasswordHash, userDto.Password) == PasswordVerificationResult.Failed)
            {
                return null!;
            }

            return _tokenService.GenerateToken(userData);
        }
    }
}
