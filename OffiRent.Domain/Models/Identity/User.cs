using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using OffiRent.Domain.Models.Booking;

namespace OffiRent.Domain.Models.Identity
{
    public class User : AuditModel
    {
        public User()
        {
            BookingIntents = new HashSet<BookingIntent>();
            Offices = new HashSet<Office>();
            Posts = new HashSet<Post>();
            Reservations = new HashSet<Reservation>();
            JoinDate = DateTime.UtcNow;
        }
        
        public bool IsPremium { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime LastOnline { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public string Token { get; set; }
        public ICollection<BookingIntent> BookingIntents { get; set; }
        public ICollection<Office> Offices { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}