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
    public class ReservationsController : ControllerBase
    {

        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }
        
        
        [SwaggerOperation(
            Summary = "Delete a reservation",
            Tags = new[] { "Reservations" })
        ]
        [SwaggerResponse(200, "Delete an reservation given it's id")]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _reservationService.CancelReservationPrematurely(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}