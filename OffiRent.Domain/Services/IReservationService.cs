using System.Collections.Generic;
using System.Threading.Tasks;
using OffiRent.Domain.Models.Booking;
using OffiRent.Domain.Response;

namespace OffiRent.Domain.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> ReservationSearch(ReservationState? reservationState = null,
            int? officeId = null, int? userId = null);
        Task<Response<Reservation>> CancelReservationPrematurely(int reservationId);
    }
}