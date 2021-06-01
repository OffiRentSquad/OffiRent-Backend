using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OffiRent.Domain.Models.Identity;
using OffiRent.Domain.Response;

namespace OffiRent.Domain.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> ListAllUsers(); 
        Task<User> GetUser(int userId);
        Task<Response<User>> RegisterUser(User user);
        Task<Response<User>> EditUser(User user);
        Task<bool> DeleteUser(User user);
        Task<bool> DeleteUser(int id);
        Task<bool> AlterPremium(int userId, bool isPremium);
    }
}