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
    public class BookingIntentService : IBookingIntentService
    {
        private readonly IRepository<BookingIntent> _bookingIntentRepository;
        private readonly IRepository<Office> _officeRepository;
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Reservation> _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookingIntentService(IRepository<BookingIntent> bookingIntentRepository,
            IRepository<Post> postRepository, IRepository<Reservation> reservationRepository,
            IRepository<Office> officeRepository, IUnitOfWork unitOfWork)
        {
            _bookingIntentRepository = bookingIntentRepository;
            _postRepository = postRepository;
            _reservationRepository = reservationRepository;
            _officeRepository = officeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<IEnumerable<BookingIntent>>> Search(int? userId = null, int? postId = null,
            BookingIntentState? bookingIntentState = null)
        {
            var query = _bookingIntentRepository.GetAll();

            if (userId is not null)
                query = query.Where(bi => bi.UserId == userId);

            if (postId is not null)
                query = query.Where(bi => bi.PostId == postId);

            if (bookingIntentState is not null)
                query = query.Where(bi => bi.BookingIntentState == bookingIntentState);
            
            return new Response<IEnumerable<BookingIntent>>(await query.ToListAsync());
        }
        
        public async Task<Response<BookingIntent>> Create(BookingIntent bookingIntent)
        {
            var post = await _postRepository.GetAsync(bookingIntent.PostId);
            if (post.PostState == PostState.Finished)
                return new Response<BookingIntent>("This post has already ended");

            if (bookingIntent.UserId == post.UserId)
                return new Response<BookingIntent>("You cannot book your own office");

            bookingIntent.BookingIntentState = BookingIntentState.Pending;
            var result = await _bookingIntentRepository.InsertAsync(bookingIntent);

            await _unitOfWork.CompleteAsync();
            return new Response<BookingIntent>(result);
        }

        public async Task<Response<BookingIntent>> SearchById(int id)
        {
            var potentialBookingIntent = await _bookingIntentRepository.GetAsync(id);
            return potentialBookingIntent is null
                ? new Response<BookingIntent>("This booking intent does not exist")
                : new Response<BookingIntent>(potentialBookingIntent);
        }

        public async Task<IEnumerable<BookingIntent>> ViewPostBookingIntents(int postId)
            => await _bookingIntentRepository.GetAll()
                .Where(bi => bi.PostId == postId).ToListAsync();

        public async Task<Response<BookingIntent>> RespondBookingIntent(int bookingIntentId, bool accept)
        {
            var bookingIntent = await _bookingIntentRepository.GetAsync(bookingIntentId);

            if (bookingIntent is null)
                return new Response<BookingIntent>("This booking intent does not exist");

            if (accept)
            {
                var post = await _postRepository.GetAll().Include(p => p.Office)
                    .FirstOrDefaultAsync(p => p.Id == bookingIntent.PostId);

                post.Office.Busy = true;
                await _officeRepository.UpdateAsync(post.Office);

                post.PostState = PostState.Finished;
                await _postRepository.UpdateAsync(post);

                var reservation = new Reservation
                {
                    BookingIntentId = bookingIntentId,
                    OfficeId = post.OfficeId,
                    StartTime = bookingIntent.ReservationProposedStartDate,
                    EndTime = bookingIntent.ReservationProposedEndDate,
                    UserId = bookingIntent.UserId,
                    ReservationState = ReservationState.Accepted
                };

                bookingIntent.BookingIntentState = BookingIntentState.Accepted;
                await _reservationRepository.InsertAsync(reservation);
            }
            else
                bookingIntent.BookingIntentState = BookingIntentState.Denied;

            var result = await _bookingIntentRepository.UpdateAsync(bookingIntent);
            await _unitOfWork.CompleteAsync();

            return new Response<BookingIntent>(result);
        }

        public async Task<Response<BookingIntent>> CancelBookingIntent(int bookingIntentId)
        {
            var bookingIntent = await _bookingIntentRepository.GetAsync(bookingIntentId);

            if (bookingIntent.BookingIntentState == BookingIntentState.Accepted)
                return new Response<BookingIntent>(
                    "This booking intent has already been accepted and cannot be cancelled");

            bookingIntent.BookingIntentState = BookingIntentState.Cancelled;

            var result = await _bookingIntentRepository.UpdateAsync(bookingIntent);
            await _unitOfWork.CompleteAsync();

            return new Response<BookingIntent>(result);
        }
    }
}