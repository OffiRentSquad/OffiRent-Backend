using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OffiRent.Api.DataObjects.Posts;
using OffiRent.Api.Extensions;
using OffiRent.Domain.Models.Booking;
using OffiRent.Domain.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OffiRent.api.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IBookingIntentService _bookingIntentService;
        private readonly IPostsService _postService;
        private readonly IMapper _mapper;

        public PostsController(IMapper mapper,
            IBookingIntentService bookingIntentService, IPostsService postService)
        {
            _mapper = mapper;
            _bookingIntentService = bookingIntentService;
            _postService = postService;
        }

        [SwaggerOperation(
            Summary = "List all posts",
            Description = "List of posts",
            OperationId = "ListAllPosts",
            Tags = new[] { "Posts" }
        )]
        [SwaggerResponse(200, "List of Posts", typeof(IEnumerable<Post>))]
        [ProducesResponseType(typeof(IEnumerable<Post>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice, [FromQuery] int? districtId, [FromQuery] bool active, [FromQuery] PostState? postState)
        {
            var result = await _postService.Search(minPrice, maxPrice, districtId, active, postState);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }

        [SwaggerOperation(
            Summary = "Create a Post",
            Description = "Create a Post",
            OperationId = "CreatePost",
            Tags = new[] { "Posts" }
        )]
        [SwaggerResponse(200, "Post was created", typeof(Post))]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] NewPost resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var post = _mapper.Map<NewPost, Post>(resource);
            var result = await _postService.Create(post);

            if (!result.Success)
                return BadRequest(result.Message);

            var created = _mapper.Map<Post, NewPost>(result.Resource);

            return Ok(created);
        }

        [SwaggerOperation(
            Summary = "Details of a post",
            Description = "Details of a post given his id",
            Tags = new[] { "Posts" }
        )]
        [SwaggerResponse(200, "Details of a post", typeof(Post))]
        [HttpGet("{postId:int}")]
        public async Task<IActionResult> GetByIdAsync(int postId, [FromQuery] int userId)
        {
            var result = await _postService.FindPost(postId, userId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }

        [SwaggerOperation(
            Summary = "Post Booking Intents",
            Description = "Booking intents of an specific Post",
            Tags = new[] { "BookingIntents", "Posts" }
        )]
        [SwaggerResponse(200, "Booking Intents of a post", typeof(IEnumerable<BookingIntent>))]
        [HttpGet("{postId:int}/booking_intents")]
        public async Task<IActionResult> GetOfficeBookingIntents(int postId, [FromQuery] BookingIntentState? bookingIntentState = null)
        {
            var result = await _bookingIntentService.Search(postId: postId, bookingIntentState: bookingIntentState);
            return Ok(result);
        }

        [SwaggerOperation(
            Summary = "Delete a post",
            Tags = new[] { "Posts" })
        ]
        [SwaggerResponse(200, "Delete an post given it's id")]
        [HttpDelete("{postId:int}")]
        public async Task<IActionResult> DeleteAsync(int postId)
        {
            await _postService.Delete(postId);
            return Ok(true);
        }
    }
}
