using TeContrato.API.Domain.Models;

namespace TeContrato.API.Domain.Services.Communications
{
    public class TechnicianResponse : BaseResponse<Technician>
    {
        public TechnicianResponse(Technician resource) : base(resource)
        {
        }

        public TechnicianResponse(string message) : base(message)
        {
        }
    }
}
