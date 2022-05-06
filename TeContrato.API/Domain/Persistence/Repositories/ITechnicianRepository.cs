using System.Collections.Generic;
using System.Threading.Tasks;
using TeContrato.API.Domain.Models;

namespace TeContrato.API.Domain.Persistence.Repositories
{
    public interface ITechnicianRepository
    {
        Task<IEnumerable<Technician>> ListAsync();
        Task AddAsync(Technician Technician);
        Task<Technician> FindById(int id);
        void Update(Technician Technician);
        void Remove(Technician Technician);
    }
}
