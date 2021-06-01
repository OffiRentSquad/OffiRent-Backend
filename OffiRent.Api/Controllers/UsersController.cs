using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using OffiRent.Api.Authentication;
using OffiRent.Api.DataObjects.Authentication;
using OffiRent.Api.DataObjects.Reservations;
using Swashbuckle.AspNetCore.Annotations;
using OffiRent.Api.DataObjects.User;
using OffiRent.Api.Extensions;
using OffiRent.Domain.Models.Booking;
using OffiRent.Domain.Models.Identity;
using OffiRent.Domain.Services;

namespace OffiRent.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IUsersService _usersService;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public UsersController(IUsersService usersService, IMapper mapper, IReservationService reservationService, IAuthenticationService authenticationService)
        {
            _usersService = usersService;
            _mapper = mapper;
            _reservationService = reservationService;
            _authenticationService = authenticationService;
        }
        
        [SwaggerOperation(
            Summary = "Authenticate with Email and Password",
            Description = "Authenticate with Email and Password",
            OperationId = "Authenticate",
            Tags = new[] {"Users"}
        )]
        [SwaggerResponse(200, "Authenticated", typeof(IEnumerable<BasicUserView>))]
        [ProducesResponseType(200)]
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request)
        {
            var response = await _authenticationService.Authenticate(request);

            if (response == null)
                return BadRequest(new { message = "Invalid Email or Password" });

            return Ok(response);
        }

        [SwaggerOperation(
            Summary = "List all users",
            Description = "List of users",
            OperationId = "ListAllUsers",
            Tags = new[] {"Users"}
        )]
        [SwaggerResponse(200, "List of Users", typeof(IEnumerable<BasicUserView>))]
        [ProducesResponseType(typeof(IEnumerable<BasicUserView>), 200)]
        [HttpGet]
        public async Task<IEnumerable<BasicUserView>> GetAllAsync()
        {
            var users = await _usersService.ListAllUsers();
            var userDataObjects = _mapper.Map<IEnumerable<User>, IEnumerable<BasicUserView>>(users);
            return userDataObjects;
        }

        [SwaggerOperation(
            Summary = "Details of a user",
            Description = "Details of a user given his id",
            Tags = new[] { "Users" }
        )]
        [SwaggerResponse(200, "Details of a user", typeof(BasicUserView))]
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetByIdAsync(int userId)
        {
            var result = await _usersService.GetUser(userId);

            if (result is null)
                return BadRequest("User not found");
            
            return Ok(_mapper.Map<User, BasicUserView>(result));
        }
        
        [SwaggerOperation(
            Summary = "Reservations of a user",
            Description = "Reservations of a user given his id",
            Tags = new[] { "Users", "Reservations" }
        )]
        [SwaggerResponse(200, "Reservations of a user", typeof(Reservation))]
        [HttpGet("{userId:int}/reservations")]
        public async Task<IActionResult> GetUserReservationsAsync(int userId,
            [FromQuery] ReservationState? reservationState = null)
        {
            
            // var result = await _usersService.EditUser(_mapper.Map<BasicUserView, User>(resource));
            // if (!result.Success)
            //     return BadRequest(result.Message);
            // return Ok(result.Resource);
            //
            var reservations = await _reservationService
                .ReservationSearch(reservationState, userId: userId);

            var dataObjects = _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationSimpleView>>(reservations);
            
            return Ok(dataObjects);
        }

        [SwaggerOperation(
            Summary = "Create a User",
            Description = "Create a User",
            OperationId = "CreateUser",
            Tags = new[] { "Users" }
        )]
        [SwaggerResponse(200, "User was created", typeof(BasicUserView))]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RegisterUser resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = _mapper.Map<RegisterUser, User>(resource);
            var result = await _usersService.RegisterUser(user);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, BasicUserView>(result.Resource);
            return Ok(userResource);
        }

        [SwaggerOperation(
            Summary = "Update a User",
            Description = "Update an User",
            OperationId = "UpdateUser",
            Tags = new[] { "Users" }
        )]
        [SwaggerResponse(200, "User was updated", typeof(BasicUserView))]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] BasicUserView resource)
        {
            
            var result = await _usersService.EditUser(_mapper.Map<BasicUserView, User>(resource));
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Resource);
        }
        
        [SwaggerOperation(
            Summary = "Delete a user",
            Tags = new[] { "Users" })
        ]
        [SwaggerResponse(200, "Delete an account given it's id")]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _usersService.DeleteUser(id);

            if (result is not true)
                return BadRequest("Could not delete");

            return Ok(true);
        }
        
    }
}