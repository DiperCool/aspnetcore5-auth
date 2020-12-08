using System.ComponentModel.DataAnnotations;

namespace Web.Models.Models
{
    public class LoginModel
    {
        [Required]
        public string Login{get;set;}
        [Required]
        public string Password{get;set;}
    }
}