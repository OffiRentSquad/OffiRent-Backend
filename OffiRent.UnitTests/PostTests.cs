using System;
using Moq;
using NUnit.Framework;
using OffiRent.Domain.Models.Booking;
using OffiRent.Domain.Models.Identity;
using OffiRent.Domain.Repository;
using OffiRent.Services;

namespace OffiRent.UnitTests
{
    public class PostTests
    {
        private static Mock<IRepository<Office>> GetDefaultIOfficeRepositoryInstance() => new();
        private static Mock<IRepository<Post>> GetDefaultIPostRepositoryInstance() => new();
        private static Mock<IRepository<User>> GetDefaultIUserRepositoryInstance() => new();
        private static Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance() => new();
        
        [Test]
        public void UserSearchesForAPost_WithNegativeMinPrice_ReturnsErrorMessage()
        {
            // Arrange
            var officeRepository = GetDefaultIOfficeRepositoryInstance();
            var postRepository = GetDefaultIPostRepositoryInstance();
            var userRepository = GetDefaultIUserRepositoryInstance();
            var unitOfWork = GetDefaultIUnitOfWorkInstance();
            var postService = new PostsService(postRepository.Object, userRepository.Object, unitOfWork.Object, officeRepository.Object);

            // Act
            const decimal minPrice = -30.2m;
            var result = postService.Search(minPrice: minPrice).Result;
            
            // Assert
            Assert.AreEqual(result.Message, "Minimum price cannot be negative");
        }
        
        [Test]
        public void UserSearchesForAPost_WithNegativeMaxPrice_ReturnsErrorMessage()
        {
            // Arrange
            var officeRepository = GetDefaultIOfficeRepositoryInstance();
            var postRepository = GetDefaultIPostRepositoryInstance();
            var userRepository = GetDefaultIUserRepositoryInstance();
            var unitOfWork = GetDefaultIUnitOfWorkInstance();
            var postService = new PostsService(postRepository.Object, userRepository.Object, unitOfWork.Object, officeRepository.Object);

            // Act
            const decimal maxPrice = -30.2m;
            var result = postService.Search(maxPrice: maxPrice).Result;
            
            // Assert
            Assert.AreEqual(result.Message, "Maximum price cannot be negative");
        }
        
        
        [Test]
        public void AddingElementsToPostCollections_WithCollectionsInitializedDuringConstructor_DoesntThrowObjectReferenceNotSetToAnInstanceOfAnObject()
        {
            // Arrange
            var post = new Post();
            var bookingIntent = new BookingIntent();
            
            var errors = false;

            // Act
            try
            {
                post.BookingIntents.Add(bookingIntent);
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