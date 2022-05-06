using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeContrato.API.Extensions;
using Swashbuckle.AspNetCore.Annotations;
using TeContrato.API.Domain.Models;
using TeContrato.API.Domain.Services;
using TeContrato.API.Resources;

namespace TeContrato.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    [Produces("application/json")]
    public class TechniciansController : ControllerBase
    {
        private readonly ITechnicianService _TechnicianService;
        private readonly IMapper _mapper;

        public TechniciansController(ITechnicianService TechnicianService, IMapper mapper)
        {
            _TechnicianService = TechnicianService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TechnicianResource>> GetAllAsync()
        {
            var tags = await _TechnicianService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Technician>, IEnumerable<TechnicianResource>>(tags) ;
            return resources;
        }
        
        [HttpGet("{CTechnician}")]
        [SwaggerOperation(Summary = "Get a Technician by Id")]
        [ProducesResponseType(typeof(TechnicianResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _TechnicianService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var TechnicianResource = _mapper.Map<Technician, TechnicianResource>(result.Resource);
            return Ok(TechnicianResource);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a Technician")]
        [ProducesResponseType(typeof(TechnicianResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveTechnicianResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var Technician = _mapper.Map<SaveTechnicianResource, Technician>(resource);
            var result = await _TechnicianService.SaveAsync(Technician);

            if (!result.Success)
                return BadRequest(result.Message);

            var TechnicianResource = _mapper.Map<Technician, TechnicianResource>(result.Resource);
            return Ok(TechnicianResource);
        }
        
        [HttpPut("{CTechnician}")]
        [SwaggerOperation(Summary = "Update a Technician by Id")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveTechnicianResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var Technician = _mapper.Map<SaveTechnicianResource, Technician>(resource);
            var result = await _TechnicianService.UpdateAsync(id, Technician);

            if (!result.Success)
                return BadRequest(result.Message);

            var TechnicianResource = _mapper.Map<Technician, TechnicianResource>(result.Resource);

            return Ok(TechnicianResource);
        }

        [HttpDelete("{CTechnician}")]
        [SwaggerOperation(Summary = "Delete a Technician by Id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _TechnicianService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var TechnicianResource = _mapper.Map<Technician, TechnicianResource>(result.Resource);

            return Ok(TechnicianResource);

        }
    }
}
