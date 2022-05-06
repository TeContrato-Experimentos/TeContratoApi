using AutoMapper;
using TeContrato.API.Domain.Models;
using TeContrato.API.Domain.Services.Communications;
using TeContrato.API.Resources;

namespace TeContrato.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, UserResource>();
            CreateMap<City, CityResource>();
            CreateMap<Client, ClientResource>();
            CreateMap<Technician, TechnicianResource>();
            CreateMap<Project, ProjectResource>();
            CreateMap<Posts, PostsResource>();
            CreateMap<ProjectControl, ProjectControlResource>();
            CreateMap<Status, StatusResource>();
            CreateMap<Budget, BudgetResource>();
            CreateMap<User, AuthenticationResponse>();

        }
    }
}
