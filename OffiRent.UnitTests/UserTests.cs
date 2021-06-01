using System;
using Moq;
using NUnit.Framework;
using OffiRent.Domain.Models.Booking;
using OffiRent.Domain.Models.Identity;
using OffiRent.Domain.Repository;
using OffiRent.Services;

namespace OffiRent.UnitTests
{
    public class UserTests
    {
        private static Mock<IRepository<User>> GetDefaultIUserRepositoryInstance() => new();
        private static Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance() => new();
        
        [Test]
        public void UserRegisters_WithFutureBirthDate_ReturnsErrorMessage()
        {
            // Arrange
            var userRepository = GetDefaultIUserRepositoryInstance();
            var unitOfWork = GetDefaultIUnitOfWorkInstance();
            var userService = new UsersService(userRepository.Object, unitOfWork.Object);
            
            // Act
            var user = new User {BirthDay = DateTime.Now.Add(TimeSpan.FromDays(1))};
            var result = userService.RegisterUser(user).Result;

            // Assert
            Assert.AreEqual(result.Message, "User birthday cannot be in the future");
        }
        
        [Test]
        public void AddingElementsToUserCollections_WithCollectionsInitializedDuringConstructor_DoesntThrowObjectReferenceNotSetToAnInstanceOfAnObject()
        {
            // Arrange
            var user = new User();
            var bookingIntent = new BookingIntent();
            var office = new Office();
            var post = new Post();
            var reservation = new Reservation();
            
            var errors = false;

            // Act
            try
            {
                user.BookingIntents.Add(bookingIntent);
                user.Offices.Add(office);
                user.Posts.Add(post);
                user.Reservations.Add(reservation);
            }
            catch (Exception e)
            {
                errors = true;
            }

            // Assert
            Assert.IsFalse(errors);
        }
    }
}