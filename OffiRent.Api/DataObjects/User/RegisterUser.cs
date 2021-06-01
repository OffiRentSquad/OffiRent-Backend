using System;
using System.Collections.Generic;

namespace OffiRent.Api.DataObjects.User
{
    public class RegisterUser
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDay { get; set; }
    }
}