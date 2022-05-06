using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeContrato.API.Domain.Models;
using TeContrato.API.Domain.Persistence.Repositories;
using TeContrato.API.Domain.Services;
using TeContrato.API.Domain.Services.Communications;

namespace TeContrato.API.Services
{
    public class TechnicianService : ITechnicianService
    {
        private readonly ITechnicianRepository _TechnicianRepository;
        public readonly IUnitOfWork _unitOfWork;


        public TechnicianService(ITechnicianRepository TechnicianRepository, IUnitOfWork unitOfWork)
        {
            _TechnicianRepository = TechnicianRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TechnicianResponse> DeleteAsync(int id)
        {
            var existingTechnician = await _TechnicianRepository.FindById(id);

            if (existingTechnician == null)
                return new TechnicianResponse("Technician not found");

            try
            {
                _TechnicianRepository.Remove(existingTechnician);
                await _unitOfWork.CompleteAsync();
                return new TechnicianResponse(existingTechnician);
            }
            catch (Exception ex)
            {
                return new TechnicianResponse($"An error ocurred while deleting technician: {ex.Message}");
            }
        }

        public async Task<TechnicianResponse> GetByIdAsync(int id)
        {
            var existingTechnician = await _TechnicianRepository.FindById(id);

            if (existingTechnician == null)
                return new TechnicianResponse("Technician not found");
            return new TechnicianResponse(existingTechnician);
        }
        
        public async Task<TechnicianResponse> SaveAsync(Technician Technician)
        {
            try
            {
                await _TechnicianRepository.AddAsync(Technician);
                await _unitOfWork.CompleteAsync();

                return new TechnicianResponse(Technician);
            }
            catch (Exception e)
            {
                return new TechnicianResponse($"Ocurrió un Error: {e.Message}");
            }
        }

        public async Task<IEnumerable<Technician>> ListAsync()
        {
            return await _TechnicianRepository.ListAsync();

        }

        public async Task<TechnicianResponse> UpdateAsync(int id, Technician Technician)
        {
            var existingTechnician = await _TechnicianRepository.FindById(id);

            if (existingTechnician == null)
                return new TechnicianResponse("Technician not found");

            existingTechnician.Cuser = Technician.Cuser;

            try
            {
                _TechnicianRepository.Update(existingTechnician);

                return new TechnicianResponse(existingTechnician);
            }
            catch (Exception ex)
            {
                return new TechnicianResponse($"An error ocurred while updating the technician: {ex.Message}");
            }

        }
    }
}
