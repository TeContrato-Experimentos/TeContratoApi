using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TeContrato.API.Domain.Models;
using TeContrato.API.Domain.Persistence.Repositories;
using TeContrato.API.Resources;
using TeContrato.API.Services;
using FluentAssertions;
using Xunit;


namespace TeContratoTesting
{
    public class TechnicianControllerTest
    {
        [Test]
        public async Task GetAllTechnicians_WhenNoTechnicians_ReturnEmptyCollection()
        {
            //Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockTechnicianRepository = GetDefaultTecnicianRepositoryInstance();

            mockTechnicianRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Technician>());

            var service = new TechnicianService(mockTechnicianRepository.Object, mockUnitOfWork.Object);
            
            //Act

            List<Technician> result = (List<Technician>) await service.ListAsync();
            var technicianCount = result.Count;
            
            // Assert

            technicianCount.Should().Equals(0);
        }
        
        private Mock<IUnitOfWork> GetDefaultUnitOfWorkRepositoryInstance()
        {
            return new Mock<IUnitOfWork>();
        }
        
        private Mock<ITechnicianRepository> GetDefaultTecnicianRepositoryInstance()
        {
            return new Mock<ITechnicianRepository>();
        }
    }
}