using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Verzel.TaskManager.WebAPI.DTO.Login;
using Verzel.TaskManager.WebAPI.Models;
using Verzel.TaskManager.WebAPI.Test.Extensions;
using Verzel.TaskManager.WebAPI.Test.Fixture;
using Xunit;

namespace Verzel.TaskManager.WebAPI.Test
{
    public class AuthControllerTest
         : IClassFixture<TestFixture<Startup>>
    {
        private readonly TestFixture<Startup> _factory;

        public AuthControllerTest(TestFixture<Startup> factory)
        {
            _factory = factory;
        }

        #region POST: /api/auth
        [Theory]
        [MemberData(nameof(ValidLogin))]
        public async Task Post_Login_Success(object payload)
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = $"/api/auth";
            var content = new StringContent(JsonConvert.SerializeObject(payload), encoding: Encoding.UTF8, mediaType: "application/json");

            // Act
            var response = await client.PostAsync(url, content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());

            var responseDTO = JsonConvert.DeserializeObject<LoginResponseDTO>(await response.Content.ReadAsStringAsync());
            Assert.NotEmpty(responseDTO.Jwt);
        }

        [Theory]
        [MemberData(nameof(InvalidLogin))]
        public async Task Post_Login_ValidationError(object payload, string errorMessage)
        {
            // Arrange
            var client = _factory.CreateClient().Authenticated();
            var url = $"/api/auth";
            var content = new StringContent(JsonConvert.SerializeObject(payload), encoding: Encoding.UTF8, mediaType: "application/json");

            // Act
            var response = await client.PostAsync(url, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("application/problem+json; charset=utf-8", response.Content.Headers.ContentType.ToString());

            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains(errorMessage, responseBody);
        }

        [Fact]
        public async Task Post_Login_NotFound()
        {
            // Arrange
            var payload = new { Email = "invalidtest@user.com", Senha = "Test" };
            var client = _factory.CreateClient().Authenticated();
            var url = $"/api/auth";
            var content = new StringContent(JsonConvert.SerializeObject(payload), encoding: Encoding.UTF8, mediaType: "application/json");

            // Act
            var response = await client.PostAsync(url, content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("application/problem+json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Post_Login_Unauthorized()
        {
            // Arrange
            var payload = new { Email = "test@user.com", Senha = "invalidPassword" };
            var client = _factory.CreateClient().Authenticated();
            var url = $"/api/auth";
            var content = new StringContent(JsonConvert.SerializeObject(payload), encoding: Encoding.UTF8, mediaType: "application/json");

            // Act
            var response = await client.PostAsync(url, content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Equal("application/problem+json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        public static IEnumerable<object[]> ValidLogin =>
            new List<object[]>
            {
                new object[] { new { Email = "test@user.com", Senha = "Test" } },
            };

        public static IEnumerable<object[]> InvalidLogin =>
            new List<object[]>
            {
                // Invalid Email        
                new object[] { new { Senha = "Test" }, "O campo email não pode ser nulo" },            
                new object[] { new { Email = (string)null, Senha = "Test" }, "O campo email não pode ser nulo" },         
                // Invalid Senha        
                new object[] { new { Email = "test@user.com" }, "O campo senha não pode ser nulo" },
                new object[] { new { Email = "test@user.com", Senha = (string) null }, "O campo senha não pode ser nulo" },
                new object[] { new { Email = "test@user.com", Senha = string.Empty }, "A senha deve conter no mínimo 3 caracteres." },
                new object[] { new { Email = "test@user.com", Senha = "1" }, "A senha deve conter no mínimo 3 caracteres." },
                new object[] { new { Email = "test@user.com", Senha = "12" }, "A senha deve conter no mínimo 3 caracteres." },
            };
        #endregion
    }
}
