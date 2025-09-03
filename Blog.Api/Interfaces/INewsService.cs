using Blog.Api.Models;

namespace Blog.Api.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<int>> GetNewsIdsAsync();

        Task<NewsContent?> GetNewsContentAsync(int id);
    }
}
