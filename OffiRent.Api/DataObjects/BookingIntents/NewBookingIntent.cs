using System;
using OffiRent.Domain.Models.Booking;

namespace OffiRent.Api.DataObjects.BookingIntents
{
    public class NewBookingIntent
    {
        public DateTime IntentDate { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime ReservationProposedStartDate { get; set; }
        public DateTime ReservationProposedEndDate { get; set; }
    }
}