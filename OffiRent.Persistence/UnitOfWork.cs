using System.Threading.Tasks;
using OffiRent.Domain.Repository;

namespace OffiRent.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OffiRentDbContext _context;

        public UnitOfWork(OffiRentDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}