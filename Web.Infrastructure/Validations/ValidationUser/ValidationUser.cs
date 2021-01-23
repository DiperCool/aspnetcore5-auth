using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Models.Db;

namespace Web.Infrastructure.Validations.ValidationUser
{
    public class ValidationUser:IValidationUser
    {
        Context _context;
        public ValidationUser(Context context)
        {
            _context = context;
        }
        public async Task<bool> userIsExist(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }
    }
}