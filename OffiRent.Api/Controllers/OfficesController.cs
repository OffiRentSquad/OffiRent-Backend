using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OffiRent.Api.DataObjects.Office;
using OffiRent.Api.Extensions;
using OffiRent.Domain.Models.Booking;
using OffiRent.Domain.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OffiRent.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OfficesController : ControllerBase
    {

        private readonly IOfficeService _officeService;
        private readonly IMapper _mapper;

        public OfficesController(IOfficeService officeService, IMapper mapper)
        {
            _officeService = officeService;
            _mapper = mapper;
        }

        [SwaggerOperation(
           Summary = "List all offices",
           Description = "List of offices",
           OperationId = "ListAllOffices",
           Tags = new[] { "Offices" }
           )]
        [SwaggerResponse(200, "List of Offices", typeof(IEnumerable<Office>))]
        [ProducesResponseType(typeof(IEnumerable<Office>), 200)]
        [HttpGet]
        public async Task<IEnumerable<Office>> GetAllAsync([FromQuery] int? userId, [FromQuery] int? districtId,
            [FromQuery] bool busy)
        {
            var offices = await _officeService.Search(userId, districtId, busy);
            
            return offices.Resource;
        }

        [SwaggerOperation(
            Summary = "Details of an Office",
            Description = "Details of an office given it's id",
            Tags = new[] { "Offices" }
        )]
        [SwaggerResponse(200, "Details of an office", typeof(Office))]
        [HttpGet("{officeId:int}")]
        public async Task<IActionResult> GetByIdAsync(int officeId)
        {
            var result = await _officeService.SearchById(officeId);

            if (result.Success is false)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [SwaggerOperation(
            Summary = "Create an Office",
            Description = "Create an Office",
            OperationId = "CreateOffice",
            Tags = new[] { "Offices" }
        )]
        [SwaggerResponse(200, "Office was created", typeof(NewOffice))]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] NewOffice resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var office = _mapper.Map<NewOffice, Office>(resource);
            var result = await _officeService.Create(office);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var newOffice = _mapper.Map<Office, NewOffice>(result.Resource);

            return Ok(newOffice);
        }

        [SwaggerOperation(
            Summary = "Update an Office",
            Description = "Update an Office",
            OperationId = "UpdateOffice",
            Tags = new[] { "Offices" }
        )]
        [SwaggerResponse(200, "Office was updated", typeof(Office))]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] Office resource)
        {
            
            var result = await _officeService.Update(resource);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Resource);
        }
        
        [SwaggerOperation(
            Summary = "Delete an office",
            Tags = new[] { "Offices" })
        ]
        [SwaggerResponse(200, "Delete an office given it's id")]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _officeService.Delete(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}