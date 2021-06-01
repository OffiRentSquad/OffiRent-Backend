using System;
using Moq;
using NUnit.Framework;
using OffiRent.Domain.Models.Booking;
using OffiRent.Domain.Models.Identity;
using OffiRent.Domain.Repository;
using OffiRent.Domain.Services;
using OffiRent.Services;

namespace OffiRent.UnitTests
{
    public class OfficeTests
    {
        private static Mock<IRepository<Office>> GetDefaultIOfficeRepositoryInstance() => new();
        private static Mock<IRepository<User>> GetDefaultIUserRepositoryInstance() => new();
        private static Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance() => new();
        
        [Test]
        public void CreatingOffice_WithUserIdBeingLessThan0_ReturnsInvalidUserResponse()
        {
            // Arrange
            var officeRepository = GetDefaultIOfficeRepositoryInstance();
            var userRepository = GetDefaultIUserRepositoryInstance();
            var unitOfWork = GetDefaultIUnitOfWorkInstance();
            var officeService = new OfficeService(officeRepository.Object, userRepository.Object, unitOfWork.Object);

            // Act
            var newOffice = new Office {UserId = -2};
            var response = officeService.Create(newOffice).Result;
            
            // Assert
            Assert.AreEqual(response.Message, "This user does not exist");
        }
        
        [Test]
        public void AddingElementsToOfficeCollections_WithCollectionsInitializedDuringConstructor_DoesntThrowObjectReferenceNotSetToAnInstanceOfAnObject()
        {
            // Arrange
            var office = new Office();
            var newPost = new Post();
            var errors = false;
            var newReservation = new Reservation();

            // Act
            try
            {
                office.Posts.Add(newPost);
                office.Reservations.Add(newReservation);
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