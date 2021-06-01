using System;
using System.Collections.Generic;
using System.ComponentModel;
using OffiRent.Domain.Models.Identity;

namespace OffiRent.Domain.Models.Booking
{
    public enum PostState: byte
    {
        [Description("Draft")]
        Draft,
        [Description("Active")]
        Active,
        [Description("Finished")]
        Finished,
        [Description("Cancelled")]
        Cancelled
    }
    
    public class Post : AuditModel
    {
        public Post()
        {
            BookingIntents = new HashSet<BookingIntent>();
        }
        
        public Office Office { get; set; }
        public int OfficeId { get; set; }
        public ICollection<BookingIntent> BookingIntents { get; set; }
        
        public User User { get; set; }
        public int UserId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Title { get; set; }
        public decimal MonthlyPrice { get; set; }

        public PostState PostState { get; set; }
    }
}