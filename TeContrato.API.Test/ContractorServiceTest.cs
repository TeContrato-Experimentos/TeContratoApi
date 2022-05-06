using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TeContrato.API.Domain.Models;
using TeContrato.API.Domain.Persistence.Repositories;
using TeContrato.API.Domain.Services.Communications;
using TeContrato.API.Services;

namespace TeContrato.API.Test
{
    public class TechnicianServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetAllAsyncWhenNoCareersReturnsEmptyCollection()
        {
            //Arrange
            var mockCareerRepository = GetDefaultITechnicianRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstace();

            mockCareerRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Technician>());

            var service = new TechnicianService(mockCareerRepository.Object, mockUnitOfWork.Object);

            //Act
            List<Technician> result = (List<Technician>)await service.ListAsync();
            var careersCount = result.Count;

            //Assert
            careersCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsCareersNotFoundResponse()
        {
            //Arrange
            var mockCareerRepository = GetDefaultITechnicianRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstace();
            var careerId = 1;
            Technician career = new Technician();
            mockCareerRepository.Setup(r => r.FindById(careerId)).Returns(Task.FromResult<Technician>(null));

            var service = new TechnicianService(mockCareerRepository.Object, mockUnitOfWork.Object);
            //Act
            TechnicianResponse result = await service.GetByIdAsync(careerId);
            var message = result.Message;
            //Assert
            message.Should().Be("Career not found");
        }
        private Mock<ITechnicianRepository> GetDefaultITechnicianRepositoryInstance()
        {
            return new Mock<ITechnicianRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstace()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}