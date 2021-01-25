using System;

namespace Web.Models.Entity
{
    public class User
    {
        public int Id{get;set;}
        public string Email{get;set;}
        public string guidEmailConfirm { get; set; }
        public bool EmailIsVerified { get; set; } = false;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password{get;set;}
        public string RefreshToken{get;set;}
        public DateTime CreatedTime{get;set;}= DateTime.Now;
    }
}