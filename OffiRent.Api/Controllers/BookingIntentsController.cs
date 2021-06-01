using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OffiRent.Api.DataObjects.BookingIntents;
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
    public class BookingIntentsController : ControllerBase
    {
        private readonly IBookingIntentService _bookingIntentService;
        private readonly IMapper _mapper;

        public BookingIntentsController(IBookingIntentService bookingIntentService, IMapper mapper)
        {
            _bookingIntentService = bookingIntentService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all Booking Intents",
            Description =
                "List of Booking Intents by filter. You could specify the user who created the booking intents to be" +
                " displayed, you can also specify the post in which these booking intents point to. There is also an" +
                "option that lets you filter these booking intents by the different states they can go through.",
            OperationId = "ListAllBookingIntents",
            Tags = new[] {"BookingIntents"}
        )]
        [SwaggerResponse(200, "List of Booking Intents", typeof(IEnumerable<BookingIntent>))]
        [ProducesResponseType(typeof(IEnumerable<BookingIntent>), 200)]
        [HttpGet]
        public async Task<IEnumerable<BookingIntent>> GetAllAsync(
            [FromQuery] BookingIntentState? bookingIntentState = null,
            [FromQuery] int? userId = null, [FromQuery] int? postId = null)
        {
            var bookingIntentsResponse = await _bookingIntentService.Search(userId, postId, bookingIntentState);
            return bookingIntentsResponse.Resource;
        }

        [SwaggerOperation(
            Summary = "Details of a Booking Intent",
            Description = "Details of a Booking Intent given it's id",
            Tags = new[] {"BookingIntents"}
        )]
        [SwaggerResponse(200, "Details of a Booking Intent", typeof(BookingIntent))]
        [HttpGet("{bookingIntentId:int}")]
        public async Task<IActionResult> GetByIdAsync(int bookingIntentId)
        {
            var result = await _bookingIntentService.SearchById(bookingIntentId);

            if (result.Success is false)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [SwaggerOperation(
            Summary = "Create a Booking Intent",
            Description = "Create an Booking Intent",
            OperationId = "CreateBookingIntent",
            Tags = new[] {"BookingIntents"}
        )]
        [SwaggerResponse(200, "Booking Intent was created", typeof(NewBookingIntent))]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] NewBookingIntent resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var bookingIntent = _mapper.Map<NewBookingIntent, BookingIntent>(resource);

            var result = await _bookingIntentService.Create(bookingIntent);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(resource);
        }

        [SwaggerOperation(
            Summary = "Update a Booking Intent",
            Description = "Update Booking Intent",
            OperationId = "UpdateBookingIntent",
            Tags = new[] {"BookingIntents"}
        )]
        [SwaggerResponse(200, "Booking Intent was updated", typeof(NewBookingIntent))]
        [HttpPatch]
        public async Task<IActionResult> PutAsync([FromQuery] int intentId, [FromQuery] bool accept)
        {
            var result = await _bookingIntentService.RespondBookingIntent(intentId, accept);
            if (!result.Success)
                return BadRequest(result.Message);

            var updatedBookingIntent = _mapper.Map<BookingIntent, NewBookingIntent>(result.Resource);

            return Ok(updatedBookingIntent);
        }

        [SwaggerOperation(
            Summary = "Cancel a Booking Intent",
            Tags = new[] {"BookingIntents"})
        ]
        [SwaggerResponse(200, "Cancel a booking intent given it's id")]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _bookingIntentService.CancelBookingIntent(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}