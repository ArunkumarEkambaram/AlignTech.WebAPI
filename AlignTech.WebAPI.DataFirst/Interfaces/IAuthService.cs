using AlignTech.WebAPI.DataFirst.DTOs;
using AlignTech.WebAPI.DataFirst.Models;

namespace AlignTech.WebAPI.DataFirst.Interfaces
{
    public interface IAuthService
    {
        Task<User> CreateUserAsync(RegisterDto registerDto);

        Task<string> LoginAsync(UserDto userDto);
    }
}
