using Blog.Api.Interfaces;
using Blog.Api.Models;
using System.Collections;

namespace Blog.Api.Services
{
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpClient;

        public NewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<NewsContent?> GetNewsContentAsync(int id)
        {
            var post = await _httpClient.GetFromJsonAsync<NewsContent>($"https://hacker-news.firebaseio.com/v0/item/{id}.json");

            return post?.Url is not null ? post : null ; 
        }

        public async Task<IEnumerable<int>> GetNewsIdsAsync()
        {
            var postsIds = await _httpClient.GetFromJsonAsync<List<int>>("https://hacker-news.firebaseio.com/v0/newstories.json");
            return postsIds ?? Enumerable.Empty<int>();
        }
    }
}
