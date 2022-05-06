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
    public class TechnicianServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllTechnicians_WhenNoTechnicians_ReturnEmptyCollection()
        {
            //Arrange
            
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockTechnicianRepository = GetDefaultTechnicianRepositoryInstance();
            mockTechnicianRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Technician>());
            var service = new TechnicianService(mockTechnicianRepository.Object, mockUnitOfWork.Object);
            
            //Act
            
            List<Technician> result = (List<Technician>) await service.ListAsync();
            var technicianCount = result.Count;
            
            // Assert
            technicianCount.Should().Be(0);
        }
        
        [Test]
        public async Task GetByIdAsync_WhenInvalidIdReturns_TechnicianNotFoundResponse()
        {
            //Arrange
            var mockTechnicianRepository = GetDefaultTechnicianRepositoryInstance();
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var technicianId = 1;
            Technician technician = new Technician();
            mockTechnicianRepository.Setup(r => r.FindById(technicianId)).Returns(Task.FromResult<Technician>(null));

            var service = new TechnicianService(mockTechnicianRepository.Object, mockUnitOfWork.Object);
            //Act
            TechnicianResponse result = await service.GetByIdAsync(technicianId);
            var message = result.Message;
            //Assert
            message.Should().Be("Technician not found");
        }

        private Mock<IUnitOfWork> GetDefaultUnitOfWorkRepositoryInstance()
        {
            return new Mock<IUnitOfWork>();
        }

        private Mock<ITechnicianRepository> GetDefaultTechnicianRepositoryInstance()
        {
            return new Mock<ITechnicianRepository>();
        }
    }
}