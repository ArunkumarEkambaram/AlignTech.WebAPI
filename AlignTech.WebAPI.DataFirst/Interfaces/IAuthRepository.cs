using AlignTech.WebAPI.DataFirst.Models;

namespace AlignTech.WebAPI.DataFirst.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> RegisterAsync(User user);

        Task<User> LoginAsync(string username);
    }
}
