using System.Collections.Generic;
using System.Threading.Tasks;
using TeContrato.API.Domain.Models;

namespace TeContrato.API.Domain.Persistence.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> ListAsync();
        Task AddAsync(Project project);
        Task<Project> FindById(int id);
        void Update(Project project);
        void Remove(Project project);

        Task<Project> FindByTechnicianIdAndBudgetId(int technicianId, int budgetId);

        Task<Project> FindByClientIdAndProjectId(int clientId, int projectId);
        
        Task<Project> FindByTechnicianIdAndProjectId(int technicianId, int projectId);


    }
}