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
    public class OfficeService : IOfficeService
    {
        private readonly IRepository<Office> _officeRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OfficeService(IRepository<Office> officeRepository,
            IRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _officeRepository = officeRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<IEnumerable<Office>>> Search(int? userId = null, int? districtId = null,
            bool busyState = false)
        {
            var query = _officeRepository.GetAll();

            if (busyState)
                query = query.Where(q => q.Busy == busyState);

            if (userId != null)
                query = query.Where(p => p.UserId == userId);

            if (districtId != null)
                query = query.Where(p => p.DistrictId == districtId);
            
            return new Response<IEnumerable<Office>>(await query.ToListAsync());
        }

        public async Task<Response<Office>> SearchById(int officeId)
        {
            var office = await _officeRepository.GetAsync(officeId);
            return office is null ? new Response<Office>("Office does not exist") 
                : new Response<Office>(office);
        }

        public async Task<Response<Office>> Create(Office office)
        {
            var user = await _userRepository.GetAsync(office.UserId);

            if (user is null)
                return new Response<Office>("This user does not exist");

            if (user.IsPremium is false)
                if (await _officeRepository.CountAsync(o => o.UserId == user.Id) > 0)
                    return new Response<Office>("Non-Premium users cannot have more than 1 office");

            if (string.IsNullOrEmpty(office.Description))
                return new Response<Office>("Office descriptions cannot be null");

            var result = await _officeRepository.InsertAsync(office);
            
            await _unitOfWork.CompleteAsync();

            return new Response<Office>(result);
        }
        
        public async Task<Response<Office>> Update(Office office)
        {
            var latestInstanceOfOffice = await _officeRepository.GetAsync(office.Id);

            if (latestInstanceOfOffice == null)
                return new Response<Office>("Office does not exist");
            
            if (latestInstanceOfOffice.Busy)
                return new Response<Office>("You cannot edit an office with an on-going Reservation");
            
            latestInstanceOfOffice.DistrictId = office.DistrictId;
            latestInstanceOfOffice.Latitude = office.Latitude;
            latestInstanceOfOffice.Longitude = office.Longitude;
            latestInstanceOfOffice.Name = office.Name;
            latestInstanceOfOffice.Description = office.Description;
                
            var result = await _officeRepository.UpdateAsync(latestInstanceOfOffice);
            await _unitOfWork.CompleteAsync();

            return new Response<Office>(result);
        }

        public async Task<Response<Office>> Delete(int officeId)
        {
            var officeToDelete = await _officeRepository.GetAsync(officeId);
            if (officeToDelete.Busy)
                return new Response<Office>("You cannot delete an office with an on-going Reservation");

            var response = new Response<Office>("Office deleted successfully") {Success = true};
            await _officeRepository.DeleteAsync(officeToDelete);
            await _unitOfWork.CompleteAsync();

            return response;
        }

    }
}