using System;
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
    public class ProjectTest
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public async Task GetByTechnicianIdAndBudgetId_WhenInvalidIds_ReturnsNotFoundResponse()
        {
            //Arrange
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockProject = GetDefaultIProjectRepositoryInstance();
            var mockClient = GetDefaultIClientRepositoryInstance();
            var mockTechnician = GetDefaultITechnicianRepositoryInstance();
            var mockBudget = GetDefaultIBudgetRepositoryInstance();
            int budgetId = 1;
            int technicianId = 1;

            mockProject.Setup(p => p.FindByTechnicianIdAndBudgetId(technicianId, budgetId))
                .Returns(Task.FromResult<Project>(null));

            var service = new ProjectService(mockProject.Object, mockClient.Object, mockTechnician.Object,
                mockBudget.Object, mockUnitOfWork.Object);
            
            //Act

            ProjectResponse result = await service.GetByTechnicianIdAndBudgetId(technicianId, budgetId);
            var message = result.Message;
            
            //Assert

            message.Should().Be("Project not found");
        }

        [Test]
        public async Task GetByTechnicianIdAndBudgetId_WhenValidIds_ReturnsProject()
        {
            //Arrange
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockProject = GetDefaultIProjectRepositoryInstance();
            var mockTechnician = GetDefaultITechnicianRepositoryInstance();
            var mockClient = GetDefaultIClientRepositoryInstance();
            var mockBudget = GetDefaultIBudgetRepositoryInstance();

            int technicianId = 1;
            int budgetId = 1;

            var projectDto = new Project
            {
                Cproject = 1,
                Nproject = "Mi Proyecto",
                Created_at = DateTime.Now,
                Tdescription = "Mi descripción",
                TechnicianId = technicianId,
                ClientId = 1,
                Mbudget = 100,
                BudgetId = budgetId
            };

            //Act
            
            mockProject.Setup(p => p.FindByTechnicianIdAndBudgetId(technicianId, budgetId))
                .Returns(Task.FromResult<Project>(projectDto));
            var service = new ProjectService(mockProject.Object, mockClient.Object, mockTechnician.Object,
                mockBudget.Object, mockUnitOfWork.Object);

            ProjectResponse result = await service.GetByTechnicianIdAndBudgetId(technicianId, budgetId);
            var projectR = result.Resource;
            
            //Assert
            
            Assert.AreEqual(projectR, projectDto);
        }

        [Test]
        public async Task GetByClientIdAndProjectId_WhenInvalidIds_ReturnsNotFoundResponse()
        {
            //Arrange
            
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockProject = GetDefaultIProjectRepositoryInstance();
            var mockClient = GetDefaultIClientRepositoryInstance();
            var mockTechnician = GetDefaultITechnicianRepositoryInstance();
            var mockBudget = GetDefaultIBudgetRepositoryInstance();
            int clientId = 1;
            int projectId = 1;

            mockProject.Setup(p => p.FindByClientIdAndProjectId(clientId, projectId))
                .Returns(Task.FromResult<Project>(null));

            var service = new ProjectService(mockProject.Object, mockClient.Object, mockTechnician.Object,
                mockBudget.Object, mockUnitOfWork.Object);
            
            //Act

            ProjectResponse result = await service.GetByClientIdAndProjectId(clientId, projectId);
            var message = result.Message;
            
            //Assert

            message.Should().Be("Project not found");
        }
        
        [Test]
        public async Task GetByTechnicianIdAndProjectId_WhenInvalidIds_ReturnsNotFoundResponse()
        {
            //Arrange
            
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockProject = GetDefaultIProjectRepositoryInstance();
            var mockClient = GetDefaultIClientRepositoryInstance();
            var mockTechnician = GetDefaultITechnicianRepositoryInstance();
            var mockBudget = GetDefaultIBudgetRepositoryInstance();
            int technicianId = 1;
            int projectId = 1;

            mockProject.Setup(p => p.FindByTechnicianIdAndProjectId(technicianId, projectId))
                .Returns(Task.FromResult<Project>(null));

            var service = new ProjectService(mockProject.Object, mockClient.Object, mockTechnician.Object,
                mockBudget.Object, mockUnitOfWork.Object);
            
            //Act

            ProjectResponse result = await service.GetByTechnicianIdAndProjectId(technicianId, projectId);
            var message = result.Message;
            
            //Assert

            message.Should().Be("Project not found");
        }


        private Mock<IProjectRepository> GetDefaultIProjectRepositoryInstance()
        {
            return new Mock<IProjectRepository>();
        }

        private Mock<IBudgetRepository> GetDefaultIBudgetRepositoryInstance()
        {
            return new Mock<IBudgetRepository>();
        }

        private Mock<ITechnicianRepository> GetDefaultITechnicianRepositoryInstance()
        {
            return new Mock<ITechnicianRepository>();
        }
        
        private Mock<IClientRepository> GetDefaultIClientRepositoryInstance()
        {
            return new Mock<IClientRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}