using Blog.Api.Models;
using Blog.Api.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Blog.Api.Controllers
{
    public static class PostsEndpoints
    {
        public static WebApplication MapPostsEndpoints(this WebApplication app)
        {
            app.MapGet("api/posts", async (IMemoryCache cache, PostService postService, int page = 1, int pageSize = 20) =>
            {
                string cacheKey = "posts_cache_key";

                if (!cache.TryGetValue(cacheKey, out List<NewsContent> posts))
                {
                    posts = await postService.GetPostsAsync();

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                    cache.Set(cacheKey, posts, cacheEntryOptions);
                }

                var totalItems = posts.Count;
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                var pagedData = posts
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var result = new
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    Data = pagedData
                };

                return result is not null ? Results.Ok(result) : Results.NotFound();
            });

            return app;
        }
    }
}
