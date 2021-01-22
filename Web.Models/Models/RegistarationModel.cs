using System.ComponentModel.DataAnnotations;

namespace Web.Models.Models
{
    public class RegistarationModel
    {
        [Required]
        [RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}", ErrorMessage ="Please provide a valid email address")]
        public string Email{get;set;}
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password{get;set;}
        [Compare("Password")]
        public string RePassword{get;set;}
    }
}