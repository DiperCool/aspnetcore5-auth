using System.Linq;
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
        public bool userIsExist(string email)
        {
            return _context.Users.Any(x => x.Email == email);
        }
    }
}