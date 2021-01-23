using System.Threading.Tasks;
using Web.Models.Entity;

namespace Web.Infrastructure.Auth
{
    public interface IAuth
    {
        Task<User> GetUser(string email, string password);
        Task<User> GetUser(int id);
        Task<User> GetUser(string email);
        Task<bool> CheckUserHaveRefreshToken(int id, string refreshToken);
        Task SaveRefreshToken(int id,string token);
        Task<User> CreateUser(User user); 
        Task SetRefreshToken(int id,string token);
        Task<string> GetRefreshToken(int id);
    }
}