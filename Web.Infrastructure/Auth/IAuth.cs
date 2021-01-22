using Web.Models.Entity;

namespace Web.Infrastructure.Auth
{
    public interface IAuth
    {
        User GetUser(string email, string password);
        User GetUser(int id);
        User GetUser(string email);
        bool CheckUserHaveRefreshToken(int id, string refreshToken);
        void SaveRefreshToken(int id,string token);
        User CreateUser(User user); 
        void SetRefreshToken(int id,string token);
        string GetRefreshToken(int id);
    }
}