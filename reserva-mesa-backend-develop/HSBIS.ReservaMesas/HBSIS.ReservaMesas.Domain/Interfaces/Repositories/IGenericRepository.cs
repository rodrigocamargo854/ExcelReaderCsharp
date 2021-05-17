using HBSIS.ReservaMesas.Domain.Entities;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task Create(T entity);
        void Update(T entity);
        Task<T> GetById(int id);
        Task Save();
    }
}
