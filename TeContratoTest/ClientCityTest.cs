using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TeContrato.API.Domain.Models;
using TeContrato.API.Domain.Persistence.Repositories;
using TeContrato.API.Domain.Services.Communications;
using TeContrato.API.Services;

namespace TeContratoTest
{
    public class ClientCityTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public async Task GetAllAsync_WhenNoClient_ReturnEmptyCollection()
        {
            //Arrange
            
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockClientRepository = GetDefaultClientRepositoryInstance();
            var mockCityRepository = new Mock<ICityRepository>();
            
            mockClientRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Client>());
            var service = new ClientService(mockClientRepository.Object, mockCityRepository.Object, mockUnitOfWork.Object);
            
            //Act
            
            List<Client> result = (List<Client>) await service.ListAsync();
            var technicianCount = result.Count;
            
            // Assert
            
            technicianCount.Should().Be(0);
        }
        
        [Test]
        public async Task GetByIdAsync_WhenInvalidIdReturns_ClientNotFoundResponse()
        {
            //Arrange
            var mockClientRepository = GetDefaultClientRepositoryInstance();
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockCityRepository = new Mock<ICityRepository>();

            var clientId = 1;
            Client client = new Client();
            mockClientRepository.Setup(r => r.FindById(clientId)).Returns(Task.FromResult<Client>(null));

            var service = new ClientService(mockClientRepository.Object, mockCityRepository.Object, mockUnitOfWork.Object);
            
            //Act
            
            ClientResponse result = await service.GetByIdAsync(clientId);
            var message = result.Message;
            
            //Assert
            message.Should().Be("Client not found");
        }

        private Mock<IUnitOfWork> GetDefaultUnitOfWorkRepositoryInstance()
        {
            return new Mock<IUnitOfWork>();
        }

        private Mock<IClientRepository> GetDefaultClientRepositoryInstance()
        {
            return new Mock<IClientRepository>();
        }
    }
}