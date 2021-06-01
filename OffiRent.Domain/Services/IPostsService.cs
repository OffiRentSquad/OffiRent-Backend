using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using OffiRent.Domain.Models.Booking;
using OffiRent.Domain.Response;

namespace OffiRent.Domain.Services
{
    public interface IPostsService
    {
        Task<IEnumerable<Post>> ListPosts();
        Task<Response<Post>> Create (Post post);

        Task<Response<IEnumerable<Post>>> Search(decimal? minPrice = null, decimal? maxPrice = null,
            int? districtId = null, bool active = false, PostState? postState = null);
        Task<Response<Post>> FindPost(int postId, int userId);
        Task Delete(int postId);
    }
}