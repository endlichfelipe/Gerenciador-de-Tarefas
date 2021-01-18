using System.Net;
using System.Threading.Tasks;
using Verzel.TaskManager.WebAPI.Test.Fixture;
using Xunit;

namespace Verzel.TaskManager.WebAPI.Test
{
    public class SecuredEndpointTest
         : IClassFixture<TestFixture<Startup>>
    {
        private readonly TestFixture<Startup> _factory;

        public SecuredEndpointTest(TestFixture<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/tarefa")]
        [InlineData("/api/tarefa/1")]
        public async Task Get_Unauthorized(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/tarefa")]
        public async Task Post_Unauthorized(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync(url, null);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/tarefa/1")]
        public async Task Put_Unauthorized(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.PutAsync(url, null);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/tarefa/1")]
        public async Task Delete_Unauthorized(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
