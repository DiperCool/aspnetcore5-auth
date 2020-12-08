using System;

namespace Web.Models.Entity
{
    public class User
    {
        public int Id{get;set;}
        public string Login{get;set;}
        public string Password{get;set;}
        public string RefreshToken{get;set;}
        public DateTime CreatedTime{get;set;}= DateTime.Now;
    }
}