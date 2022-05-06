using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeContrato.API.Domain.Models;
using TeContrato.API.Domain.Persistence.Contexts;
using TeContrato.API.Domain.Persistence.Repositories;

namespace TeContrato.API.Persistence.Repositories
{
    public class TechnicianRepository : BaseRepository, ITechnicianRepository
    {

        public TechnicianRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Technician>> ListAsync()
        {
            return await _context.Technicians.ToListAsync();
        }

        public async Task AddAsync(Technician Technician)
        {
            await _context.AddAsync(Technician);
        }

        public async Task<Technician> FindById(int id)
        {
            return await _context.Technicians.FindAsync(id);
        }

        public void Remove(Technician Technician)
        {
            _context.Remove(Technician);
        }
        public void Update(Technician Technician)
        {
            _context.Update(Technician);
        }
    }
}
