using Blog.Api.Interfaces;
using Blog.Api.Models;
using Blog.Api.Services;
using Moq;

namespace Blog.Tests
{
    public class PostServiceTests
    {

        [Fact]
        public async Task GetAllNewsAsync_ReturnsNewsWithDetails()
        {
            // Arrange
            var mockApi = new Mock<INewsService>();
            mockApi.Setup(x => x.GetNewsIdsAsync()).ReturnsAsync(new List<int> { 1, 2 });
            mockApi.Setup(x => x.GetNewsContentAsync(1)).ReturnsAsync(new NewsContent { Id = 1, Title = "Title One", Url = "url1" });
            mockApi.Setup(x => x.GetNewsContentAsync(2)).ReturnsAsync(new NewsContent { Id = 2, Title = "Title Two", Url = "url2" });

            var service = new PostService(mockApi.Object);

            // Act
            var result = await service.GetPostsAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, r => r.Id == 1 && r.Title == "Title One" && r.Url == "url1");
            Assert.Contains(result, r => r.Id == 2 && r.Title == "Title Two" && r.Url == "url2");
        }
    }
}
