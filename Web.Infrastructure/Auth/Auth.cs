using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> CheckUserHaveRefreshToken(int id, string refreshToken)
        {
            User user= await _context.Users.FirstOrDefaultAsync(x=>x.RefreshToken==refreshToken&&x.Id==id);
            if(user==null) return false;
            return true;
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUser(string email, string password)
        {
            User user =await _context.Users.FirstOrDefaultAsync(x=>x.Email==email&&x.Password==password);
            return user;
        }
        public async Task<User> GetUser(int id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x=>x.Id==id);
            return user;
        }

        public async Task SaveRefreshToken(int id, string token)
        {
            User user= await _context.Users.FirstOrDefaultAsync(x=>x.Id==id);
            if(user==null) return;
            user.RefreshToken=token;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task SetRefreshToken(int id, string token)
        {
            User user= await _context.Users.FirstOrDefaultAsync(x=>x.Id==id);
            user.RefreshToken = token;
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public async Task<string> GetRefreshToken(int id)
        {
            return (await _context.Users.FirstOrDefaultAsync(x => x.Id == id))?.RefreshToken;
        }

        public  async Task<User> GetUser(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x=>x.Email==email);
        }
    }
}