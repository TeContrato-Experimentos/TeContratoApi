using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeContrato.API.Domain.Models;
using TeContrato.API.Domain.Persistence.Contexts;
using TeContrato.API.Domain.Persistence.Repositories;

namespace TeContrato.API.Persistence.Repositories
{
    public class ProjectRepository : BaseRepository, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Project>> ListAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task AddAsync(Project project)
        {
            await _context.AddAsync(project);
        }

        public async Task<Project> FindById(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public void Remove(Project project)
        {
            _context.Remove(project);
        }
        public void Update(Project project)
        {
            _context.Update(project);
        }

        public async Task<Project> FindByTechnicianIdAndBudgetId(int technicianId, int budgetId)
        {
            return await _context.Projects.FindAsync(technicianId, budgetId);
        }

        public async Task<Project> FindByClientIdAndProjectId(int clientId, int projectId)
        {
            return await _context.Projects.FindAsync(clientId, projectId);
        }
        
        public async Task<Project> FindByTechnicianIdAndProjectId(int technicianId, int projectId)
        {
            return await _context.Projects.FindAsync(technicianId, projectId);
        }
    }
}