using System.Threading.Tasks;

namespace Web.Infrastructure.Validations.ValidationUser
{
    public interface IValidationUser
    {
        Task<bool> userIsExist(string email);
    }
}