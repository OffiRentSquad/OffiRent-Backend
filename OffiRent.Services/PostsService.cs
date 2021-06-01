using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OffiRent.Domain.Models.Booking;
using OffiRent.Domain.Models.Identity;
using OffiRent.Domain.Repository;
using OffiRent.Domain.Response;
using OffiRent.Domain.Services;

namespace OffiRent.Services
{
    public class PostsService : IPostsService
    {
        private readonly IRepository<Office> _officeRepository;
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PostsService(IRepository<Post> postRepository,
            IRepository<User> userRepository, IUnitOfWork unitOfWork, IRepository<Office> officeRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _officeRepository = officeRepository;
        }

        public async Task<IEnumerable<Post>> ListPosts()
            => await _postRepository.GetAll().ToListAsync();

        public async Task<Response<Post>> Create(Post post)
        {
            if (post.OfficeId < 1)
                return new Response<Post>("Post must be associated to an office");

            var office = await _officeRepository.GetAsync(post.OfficeId);

            if (office is null)
                return new Response<Post>("Specified office does not exist");

            if (office.UserId != post.UserId)
                return new Response<Post>("You cannot create posts over an office that is not yours");

            var user = await _userRepository.GetAsync(post.UserId);
            if (user.IsPremium is false)
                if (post.EndDate.Subtract(post.StartDate) > TimeSpan.FromDays(30))
                    return new Response<Post>("Non-Premium users cannot make posts that last more than 30 days");

            post.PostState = PostState.Active;
            var result = await _postRepository.InsertAsync(post);
            await _unitOfWork.CompleteAsync();

            return new Response<Post>(result);
        }

        public async Task<Response<IEnumerable<Post>>> Search(decimal? minPrice = null, decimal? maxPrice = null,
            int? districtId = null, bool active = false, PostState? postState = null)
        {
            var query = _postRepository.GetAll();

            if (active)
                query = query.Where(p => p.PostState == PostState.Active);

            if (minPrice != null)
            {
                query = query.Where(p => p.MonthlyPrice >= minPrice);
                if (minPrice < 0)
                    return new Response<IEnumerable<Post>>("Minimum price cannot be negative");
            }

            if (maxPrice != null)
            {
                query = query.Where(p => p.MonthlyPrice <= maxPrice);
                if (maxPrice < 0)
                    return new Response<IEnumerable<Post>>("Maximum price cannot be negative");
            }

            if (postState is not null)
            {
                query = query.Where(q => q.PostState == postState);
            }

            if (districtId != null)
            {
                query.Include(p => p.Office);
                query = query.Where(p => p.Office.DistrictId == districtId);
            }

            return new Response<IEnumerable<Post>>(await query.ToListAsync());
        }

        public async Task<Response<Post>> FindPost(int postId, int userId)
        {
            var potentialPost = await _postRepository.FirstOrDefaultAsync(p => p.Id == postId);

            if (potentialPost is null)
                return new Response<Post>("This post could not be found");

            if (potentialPost.UserId == userId)
            {
                if (potentialPost.IsDeleted)
                    return new Response<Post>("This post has been deleted");
            }
            else
                if (potentialPost.PostState != PostState.Active)
                return new Response<Post>("This post could not be found");

            return new Response<Post>(potentialPost);
        }

        public async Task Delete(int postId)
        {
            await _postRepository.DeleteAsync(postId);
            await _unitOfWork.CompleteAsync();
        }
    }
}
