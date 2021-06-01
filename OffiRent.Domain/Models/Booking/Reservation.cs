using System;
using System.ComponentModel;
using OffiRent.Domain.Models.Identity;

namespace OffiRent.Domain.Models.Booking
{
    public enum ReservationState : byte
    {
        [Description("Accepted")]
        Accepted,
        [Description("Active")]
        Active,
        [Description("Cancelled")]
        Cancelled,
        [Description("Finished")]
        Finished
    }
    
    public class Reservation : AuditModel
    {
        public BookingIntent BookingIntent { get; set; }
        public int BookingIntentId { get; set; }
        
        public Office Office { get; set; }
        public int OfficeId { get; set; }
        
        public User User { get; set; }
        public int UserId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ReservationState ReservationState { get; set; }
    }
}