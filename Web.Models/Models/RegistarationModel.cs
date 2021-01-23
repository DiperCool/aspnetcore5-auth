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
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$@!%&*?])[A-Za-z\d#$@!%&*?]{8,30}$", 
                ErrorMessage="min 1 uppercase, min 1 lowercase, min 1 special character, min 1 number, min 8 characters, max 30 characters")]

        public string Password{get;set;}
        [Compare("Password")]
        public string RePassword{get;set;}
    }
}