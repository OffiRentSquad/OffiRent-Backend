using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OffiRent.Domain.Models.Booking;
using OffiRent.Domain.Repository;
using OffiRent.Domain.Response;
using OffiRent.Domain.Services;

namespace OffiRent.Services
{
    public class ReservationsService : IReservationService
    {
        private readonly IRepository<Office> _officeRepository;
        private readonly IRepository<Reservation> _reservationsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReservationsService(IRepository<Reservation> reservationsRepository,
            IRepository<Office> officeRepository, IUnitOfWork unitOfWork)
        {
            _reservationsRepository = reservationsRepository;
            _officeRepository = officeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Reservation>> ReservationSearch(ReservationState? reservationState = null,
            int? officeId = null, int? userId = null)
        {
            var query = _reservationsRepository.GetAll();
            if (reservationState is not null)
                query = query.Where(q => q.ReservationState == reservationState);

            if (officeId is not null)
                query = query.Where(q => q.OfficeId == officeId);
            
            if (userId is not null)
                query = query.Where(q => q.UserId == userId);

            var result = await query
                .Include(r => r.User)
                .Include(r => r.Office)
                .Include(r => r.BookingIntent)
                .ToListAsync();
            return result;
        }
        
        public async Task<Response<Reservation>> CancelReservationPrematurely(int reservationId)
        {
            var reservation = await _reservationsRepository.GetAsync(reservationId);

            var office = await _officeRepository.GetAsync(reservation.OfficeId);
            office.Busy = false;
            await _officeRepository.UpdateAsync(office);

            if (reservation.ReservationState == ReservationState.Finished)
                return new Response<Reservation>("This reservation has already finished");
            
            reservation.ReservationState = ReservationState.Cancelled;

            var result = await _reservationsRepository.UpdateAsync(reservation);
            await _unitOfWork.CompleteAsync();

            return new Response<Reservation>(result);
        }
    }
}