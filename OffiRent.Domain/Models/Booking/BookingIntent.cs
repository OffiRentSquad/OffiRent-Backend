#nullable enable
using System;
using System.ComponentModel;
using OffiRent.Domain.Models.Identity;

namespace OffiRent.Domain.Models.Booking
{
    public enum BookingIntentState : byte
    {
        [Description("Pending")]
        Pending,
        [Description("Accepted")]
        Accepted,
        [Description("Denied")]
        Denied,
        [Description("Cancelled")]
        Cancelled
    }
    
    public class BookingIntent : AuditModel
    {
        public DateTime IntentDate { get; set; }

        
        /// <summary>
        /// Analysis based on Posts is more interesting
        /// than based on Offices themselves. Maybe a Post
        /// had something that attracted more people
        /// </summary>

        public Post Post { get; set; }
        public int PostId { get; set; }
        public Reservation? Reservation { get; set; }
        public int? ReservationId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime ReservationProposedStartDate { get; set; }
        public DateTime ReservationProposedEndDate { get; set; }
        public BookingIntentState BookingIntentState { get; set; }
    }
}