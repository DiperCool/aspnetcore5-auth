using System.Threading.Tasks;

namespace Web.Infrastructure.EmailSender
{
    public interface IEmailSender
    {
        Task SendConfirmEmail(string email, string url);
    }
}