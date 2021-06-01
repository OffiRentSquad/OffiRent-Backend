using System.Collections.Generic;
using System.Threading.Tasks;
using OffiRent.Domain.Models.Booking;
using OffiRent.Domain.Response;

namespace OffiRent.Domain.Services
{
    public interface IBookingIntentService

    {
        Task<Response<IEnumerable<BookingIntent>>> Search(int? userId = null, int? postId = null,
            BookingIntentState? bookingIntentState = null);
        Task<Response<BookingIntent>> Create(BookingIntent bookingIntent);
        Task<Response<BookingIntent>> SearchById(int id);
        Task<IEnumerable<BookingIntent>> ViewPostBookingIntents(int postId);
        Task<Response<BookingIntent>> RespondBookingIntent(int bookingIntentId, bool accept);
        Task<Response<BookingIntent>> CancelBookingIntent(int bookingIntentId);
    }
}