using AutoMapper;
using TeContrato.API.Domain.Models;
using TeContrato.API.Resources;

namespace TeContrato.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveUserResource, User>();
            CreateMap<SaveClientResource, Client>();
            CreateMap<SaveCityResource, City>();
            CreateMap<SaveTechnicianResource, Technician>();
            CreateMap<SavePostsResource, Posts>();
            CreateMap<SaveProjectResource, Project>();
            CreateMap<SaveProjectControlResource, ProjectControl>();
            CreateMap<SaveStatusResource, Status>();
            CreateMap<SaveBudgetResource, Budget>();

        }
    }
}