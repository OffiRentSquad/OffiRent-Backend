using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OffiRent.Domain.Models.Booking;
using OffiRent.Domain.Models.Identity;
using OffiRent.Domain.Repository;
using OffiRent.Domain.Response;
using OffiRent.Domain.Services;

namespace OffiRent.Services
{
    public class UsersService : IUsersService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;


        public UsersService(IRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> ListAllUsers()
        {
            var users = await _userRepository.GetAll().ToListAsync();
            return users;
        }

        public async Task<User> GetUser(int userId)
            =>
                await _userRepository.GetAsync(userId);

        public async Task<Response<User>> RegisterUser(User user)
        {
            if (user.Id != 0)
                return new Response<User>("This user already exists");

            if (await  _userRepository.CountAsync(u => u.Email == user.Email) > 0)
                return new Response<User>("This email is already being used by another account");
            
            if (user.BirthDay > DateTime.UtcNow)
                return new Response<User>("User birthday cannot be in the future");
            
            user.JoinDate = DateTime.UtcNow;
            user.LastOnline = DateTime.UtcNow;
            
            await _userRepository.InsertAsync(user);
            await _unitOfWork.CompleteAsync();

            return new Response<User>(user);
        }

        public async Task<Response<User>> EditUser(User user)
        {
            try
            {
                if(await _userRepository.Contains(user) is not true)
                    return new Response<User>($"The user does not exist");
                
                await _userRepository.UpdateAsync(user);
                await _unitOfWork.CompleteAsync();

                return new Response<User>(user);
            }
            catch (Exception ex)
            {
                return new Response<User>($"An error occurred while updating the User: {ex.Message}");
            }
        }

        public async Task<bool> DeleteUser(User user)
        {
            try
            {
                if (await _userRepository.Contains(user) is not true)
                    return false;
                
                // Which one will it be?
                await _userRepository.DeleteAsync(user);
                await _unitOfWork.CompleteAsync();
        
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var user = await _userRepository.GetAsync(id);
                if (user is null)
                    return false;
                
                // Which one will it be?
                await _userRepository.DeleteAsync(user);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public async Task<bool> AlterPremium(int userId, bool isPremium)
        {
            var user = await _userRepository.GetAsync(userId);
            
            user.IsPremium = isPremium;
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}