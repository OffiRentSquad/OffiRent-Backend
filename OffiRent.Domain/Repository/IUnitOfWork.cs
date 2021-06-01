using System.Threading.Tasks;

namespace OffiRent.Domain.Repository
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}