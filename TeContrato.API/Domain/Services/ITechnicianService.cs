using System.Collections.Generic;
using System.Threading.Tasks;
using TeContrato.API.Domain.Models;
using TeContrato.API.Domain.Services.Communications;

namespace TeContrato.API.Domain.Services
{
    public interface ITechnicianService
    {
        Task<IEnumerable<Technician>> ListAsync();
        Task<TechnicianResponse> GetByIdAsync(int id);
        Task<TechnicianResponse> SaveAsync(Technician Technician);
        Task<TechnicianResponse> UpdateAsync(int id, Technician Technician);
        Task<TechnicianResponse> DeleteAsync(int id);
    }
}
