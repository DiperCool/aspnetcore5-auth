using System.Linq;
using Web.Models.Db;
using Web.Models.Entity;

namespace Web.Infrastructure.Auth
{
    public class Auth: IAuth
    {
        private Context _context;
        public Auth(Context context)
        {
            _context=context;
        }

        public bool CheckUserHaveRefreshToken(int id, string refreshToken)
        {
            User user= _context.Users.FirstOrDefault(x=>x.RefreshToken==refreshToken&&x.Id==id);
            if(user==null) return false;
            return true;
        }

        public User CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User GetUser(string email, string password)
        {
            User user = _context.Users.FirstOrDefault(x=>x.Email==email&&x.Password==password);
            return user;
        }public User GetUser(int id)
        {
            User user = _context.Users.FirstOrDefault(x=>x.Id==id);
            return user;
        }

        public void SaveRefreshToken(int id, string token)
        {
            User user= _context.Users.FirstOrDefault(x=>x.Id==id);
            if(user==null) return;
            user.RefreshToken=token;
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void SetRefreshToken(int id, string token)
        {
            User user= _context.Users.FirstOrDefault(x=>x.Id==id);
            user.RefreshToken = token;
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public string GetRefreshToken(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id)?.RefreshToken;
        }

        public User GetUser(string email)
        {
            return _context.Users.FirstOrDefault(x=>x.Email==email);
        }
    }
}