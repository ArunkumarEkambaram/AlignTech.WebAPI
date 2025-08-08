using AlignTech.WebAPI.DataFirst.Models;

namespace AlignTech.WebAPI.DataFirst.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
