using System;
using OffiRent.Api.DataObjects.BookingIntents;
using OffiRent.Api.DataObjects.Office;
using OffiRent.Api.DataObjects.User;
using OffiRent.Domain.Models.Booking;

namespace OffiRent.Api.DataObjects.Reservations
{
    public class ReservationSimpleView
    {
        public int Id { get; set; }
        public NewBookingIntent BookingIntent { get; set; }
        
        public NewOffice Office { get; set; }
        
        public BasicUserView User { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ReservationState ReservationState { get; set; }
    }
}