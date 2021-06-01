using System.Collections.Generic;
using System.Threading.Tasks;
using OffiRent.Domain.Models.Booking;
using OffiRent.Domain.Response;

namespace OffiRent.Domain.Services
{
    public interface IOfficeService
    {
        Task<Response<IEnumerable<Office>>> Search(int? userId = null, int? districtId = null,
            bool busyState = false);
        public Task<Response<Office>> SearchById (int officeId);
        public Task<Response<Office>> Create (Office office);
        public Task<Response<Office>> Update (Office office);
        public Task<Response<Office>> Delete (int officeId);

    }
}