using Blog.Api.Interfaces;
using Blog.Api.Models;

namespace Blog.Api.Services
{
    public class PostService
    {
        private readonly INewsService _newsService;

        public PostService(INewsService newsService)
        {
            _newsService = newsService;   
        }

        public async Task<List<NewsContent?>> GetPostsAsync()
        {
            var ids = await _newsService.GetNewsIdsAsync();

            var tasks = ids.Select(id => _newsService.GetNewsContentAsync(id));

            var result = await Task.WhenAll(tasks);

            return result.ToList();
        }
    }
}
