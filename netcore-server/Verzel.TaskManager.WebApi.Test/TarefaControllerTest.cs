using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Verzel.TaskManager.WebAPI.Models;
using Verzel.TaskManager.WebAPI.Test.Factory;
using Xunit;

namespace Verzel.TaskManager.WebAPI.Test
{
    public class TarefaControllerTest
         : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public TarefaControllerTest(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        #region GET: /api/tarefa
        [Fact]
        public async Task Get_GetAllTarefas_Success()
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = "/api/tarefa";

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());

            var list = JsonConvert.DeserializeObject<IEnumerable<Tarefa>>(await response.Content.ReadAsStringAsync());
            Assert.NotEmpty(list);
        }
        #endregion

        #region GET: /api/tarefa/{id}
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Get_GetTarefaById_Success(long id)
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = $"/api/tarefa/{id}";

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());

            var tarefa = JsonConvert.DeserializeObject<Tarefa>(await response.Content.ReadAsStringAsync());
            Assert.Equal(id, tarefa.Id);
        }

        [Fact]
        public async Task Get_GetTarefaById_NotFound()
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = $"/api/tarefa/0";

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("application/problem+json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
        #endregion

        #region POST: /api/tarefa
        [Theory]
        [MemberData(nameof(ValidCreateTarefa))]
        public async Task Post_CreateTarefa_Success(object payload)
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = $"/api/tarefa";
            var content = new StringContent(JsonConvert.SerializeObject(payload), encoding: Encoding.UTF8, mediaType: "application/json");

            // Act
            var response = await client.PostAsync(url, content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [MemberData(nameof(InvalidCreateTarefa))]
        public async Task Post_CreateTarefa_ValidationError(object payload, string errorMessage)
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = $"/api/tarefa";
            var content = new StringContent(JsonConvert.SerializeObject(payload), encoding: Encoding.UTF8, mediaType: "application/json");

            // Act
            var response = await client.PostAsync(url, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("application/problem+json; charset=utf-8", response.Content.Headers.ContentType.ToString());

            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains(errorMessage, responseBody);
        }

        public static IEnumerable<object[]> ValidCreateTarefa =>
            new List<object[]>
            {
                new object[] { new { Descricao = "some description", DataConclusao = DateTime.Now, DataEntrega =  DateTime.Now } },
                new object[] { new { Descricao = "some description", DataConclusao = DateTime.Now.AddYears(1), DataEntrega =  DateTime.Now } },
                new object[] { new { Descricao = "some description", DataConclusao = DateTime.Now, DataEntrega =  DateTime.Now.AddYears(1) } },
                new object[] { new { Descricao = "some description", DataConclusao = "2020-02-02", DataEntrega =  "2020-02-02" } },
            };

        public static IEnumerable<object[]> InvalidCreateTarefa =>
            new List<object[]>
            {
                // Invalid Descrição        
                new object[] { new { DataConclusao = DateTime.Now, DataEntrega =  DateTime.Now },  "O campo descricao não pode ser nulo"},
                new object[] { new { Descricao = (string) null, DataConclusao = DateTime.Now, DataEntrega =  DateTime.Now },  "O campo descricao não pode ser nulo"},
                new object[] { new { Descricao = string.Empty, DataConclusao = DateTime.Now, DataEntrega =  DateTime.Now },  "A descrição deve conter no mínimo 3 caracteres."},
                new object[] { new { Descricao = "1", DataConclusao = DateTime.Now, DataEntrega =  DateTime.Now },  "A descrição deve conter no mínimo 3 caracteres."},
                new object[] { new { Descricao = "12", DataConclusao = DateTime.Now, DataEntrega =  DateTime.Now },  "A descrição deve conter no mínimo 3 caracteres."},
            };
        #endregion

        #region PUT: /api/tarefa/{id}
        [Theory]
        [MemberData(nameof(ValidUpdateTarefa))]
        public async Task Put_UpdateTarefa_Success(long id, object payload)
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = $"/api/tarefa/{id}";
            var content = new StringContent(JsonConvert.SerializeObject(payload), encoding: Encoding.UTF8, mediaType: "application/json");

            // Act
            var response = await client.PutAsync(url, content);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [MemberData(nameof(InvalidUpdateTarefa))]
        public async Task Put_UpdateTarefa_ValidationError(long id, object payload, string errorMessage)
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = $"/api/tarefa/{id}";
            var content = new StringContent(JsonConvert.SerializeObject(payload), encoding: Encoding.UTF8, mediaType: "application/json");

            // Act
            var response = await client.PutAsync(url, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("application/problem+json; charset=utf-8", response.Content.Headers.ContentType.ToString());

            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains(errorMessage, responseBody);
        }

        [Fact]
        public async Task Put_UpdateTarefa_WrongId()
        {
            // Arrange
            var id = long.MaxValue;
            var payload = new { Id = 1, Descricao = "some description", DataConclusao = DateTime.Now, DataEntrega = DateTime.Now };
            var client = _factory.CreateClient();
            var url = $"/api/tarefa/{id}";
            var content = new StringContent(JsonConvert.SerializeObject(payload), encoding: Encoding.UTF8, mediaType: "application/json");

            // Act
            var response = await client.PutAsync(url, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("application/problem+json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Put_UpdateTarefa_NotFound()
        {
            // Arrange
            var id = long.MaxValue;
            var payload = new { Id = long.MaxValue, Descricao = "some description", DataConclusao = DateTime.Now, DataEntrega = DateTime.Now };
            var client = _factory.CreateClient();
            var url = $"/api/tarefa/{id}";
            var content = new StringContent(JsonConvert.SerializeObject(payload), encoding: Encoding.UTF8, mediaType: "application/json");

            // Act
            var response = await client.PutAsync(url, content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("application/problem+json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        public static IEnumerable<object[]> ValidUpdateTarefa =>
            new List<object[]>
            {
                new object[] { 1, new { Id=1, Descricao = "some description", DataConclusao = DateTime.Now, DataEntrega =  DateTime.Now } },
                new object[] { 1, new { Id=1, Descricao = "some description", DataConclusao = DateTime.Now.AddYears(1), DataEntrega =  DateTime.Now } },
                new object[] { 1, new { Id=1, Descricao = "some description", DataConclusao = DateTime.Now, DataEntrega =  DateTime.Now.AddYears(1) } },
                new object[] { 1, new { Id=1, Descricao = "some description", DataConclusao = "2020-02-02", DataEntrega =  "2020-02-02" } },
            };

        public static IEnumerable<object[]> InvalidUpdateTarefa =>
            new List<object[]>
            {
                // Invalid Descrição        
                new object[] { 1, new { Id=1, DataConclusao = DateTime.Now, DataEntrega =  DateTime.Now },  "O campo descricao não pode ser nulo"},
                new object[] { 1, new { Id=1, Descricao = (string) null, DataConclusao = DateTime.Now, DataEntrega =  DateTime.Now },  "O campo descricao não pode ser nulo"},
                new object[] { 1, new { Id=1, Descricao = string.Empty, DataConclusao = DateTime.Now, DataEntrega =  DateTime.Now },  "A descrição deve conter no mínimo 3 caracteres."},
                new object[] { 1, new { Id=1, Descricao = "1", DataConclusao = DateTime.Now, DataEntrega =  DateTime.Now },  "A descrição deve conter no mínimo 3 caracteres."},
                new object[] { 1, new { Id=1, Descricao = "12", DataConclusao = DateTime.Now, DataEntrega =  DateTime.Now },  "A descrição deve conter no mínimo 3 caracteres."},
                // Invalid Id
                new object[] { 1, new { Id=-1, Descricao = "some description", DataConclusao = DateTime.Now, DataEntrega =  DateTime.Now }, "ID deve ser maior do que zero" },
                new object[] { 1, new { Id=0, Descricao = "some description", DataConclusao = DateTime.Now, DataEntrega =  DateTime.Now }, "ID deve ser maior do que zero" },
            };
        #endregion

        #region DELETE: /api/tarefa/{id}
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Delete_DeleteTarefa_Success(long id)
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = $"/api/tarefa/{id}";

            // Act
            var response = await client.DeleteAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Delete_DeleteTarefa_NotFound()
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = $"/api/tarefa/0";

            // Act
            var response = await client.DeleteAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("application/problem+json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
        #endregion
    }
}
