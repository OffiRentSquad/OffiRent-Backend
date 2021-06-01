using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using OffiRent.Domain.Models.Booking;
using OffiRent.Domain.Repository;
using OffiRent.Services;

namespace OffiRent.UnitTests
{
    public class BookingIntentTests
    {
        // Pipeline test
        private static Mock<IRepository<BookingIntent>> GetDefaultIBookingIntentRepositoryInstance() => new();
        private static Mock<IRepository<Office>> GetDefaultIOfficeRepositoryInstance() => new();
        private static Mock<IRepository<Post>> GetDefaultIPostRepositoryInstance() => new();
        private static Mock<IRepository<Reservation>> GetDefaultIReservationRepositoryInstance() => new();
        private static Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance() => new();

        [Test]
        public void CancelBookingIntent_ThatHasBeenAccepted_CannotBeCancelled()
        {
            // Arrange
            var officeRepository = GetDefaultIOfficeRepositoryInstance();
            var postRepository = GetDefaultIPostRepositoryInstance();
            var bookingIntentRepository = GetDefaultIBookingIntentRepositoryInstance();

            var stubBookingIntent = new BookingIntent { Id = 1, BookingIntentState = BookingIntentState.Accepted };
            bookingIntentRepository.Setup(bi => bi.GetAsync(stubBookingIntent.Id))
                .Returns(Task.FromResult(stubBookingIntent));
            
            var reservationIntentRepository = GetDefaultIReservationRepositoryInstance();
            var unitOfWork = GetDefaultIUnitOfWorkInstance();
            
            
            var bookingIntentService = new BookingIntentService(bookingIntentRepository.Object, postRepository.Object,
            reservationIntentRepository.Object, officeRepository.Object, unitOfWork.Object);

            // Act
            const int idOfBookingIntentToBeCancelled = 1;
            var result = bookingIntentService.CancelBookingIntent(idOfBookingIntentToBeCancelled).Result;

            // Assert
            Assert.AreEqual(result.Message, "This booking intent has already been accepted and cannot be cancelled");
        }

    }
}