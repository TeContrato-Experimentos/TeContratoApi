using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeContrato.API.Domain.Models;
using TeContrato.API.Domain.Persistence.Repositories;
using TeContrato.API.Domain.Services;
using TeContrato.API.Domain.Services.Communications;
using BCryptNet = BCrypt.Net;


namespace TeContrato.API.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICityRepository _cityRepository;
        public readonly IUnitOfWork _unitOfWork;
        
        public ClientService(IClientRepository clientRepository, ICityRepository cityRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ClientResponse> DeleteAsync(int id)
        {
            var existingClient = await _clientRepository.FindById(id);

            if (existingClient == null)
                return new ClientResponse("Client not found");

            try
            {
                _clientRepository.Remove(existingClient);
                await _unitOfWork.CompleteAsync();
                return new ClientResponse(existingClient);
            }
            catch (Exception ex)
            {
                return new ClientResponse($"An error ocurred while deleting Client: {ex.Message}");
            }
        }

        public async Task<ClientResponse> GetByIdAsync(int id)
        {
            var existingClient = await _clientRepository.FindById(id);

            if (existingClient == null)
                return new ClientResponse("Client not found");
            return new ClientResponse(existingClient);
        }
        
        public async Task<ClientResponse> SaveAsync(int cityId, Client client)
        {
            var existingCity = await _cityRepository.FindById(cityId);

            if (existingCity == null)
                return new ClientResponse("Client not found");
            
            try
            {
                client.CityId = cityId;
                client.Cpassword = BCryptNet.BCrypt.HashPassword(client.Cpassword);
                await _clientRepository.AddAsync(client);
                await _unitOfWork.CompleteAsync();
                return new ClientResponse(client);
            }
            catch (Exception e)
            {
                return new ClientResponse($"Ocurrió un Error: {e.Message}");
            }
        }

        public async Task<IEnumerable<Client>> ListAsync()
        {
            return await _clientRepository.ListAsync();

        }

        public async Task<ClientResponse> UpdateAsync(int id, Client client)
        {
            var existingClient = await _clientRepository.FindById(id);

            if (existingClient == null)
                return new ClientResponse("Client not found");

            existingClient.Cuser = client.Cuser;

            try
            {
                _clientRepository.Update(existingClient);

                return new ClientResponse(existingClient);
            }
            catch (Exception ex)
            {
                return new ClientResponse($"An error ocurred while updating the Client: {ex.Message}");
            }

        }
    }
}
