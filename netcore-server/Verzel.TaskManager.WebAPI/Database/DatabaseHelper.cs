using Microsoft.Extensions.DependencyInjection;
using System;
using Verzel.TaskManager.WebAPI.Models;

namespace Verzel.TaskManager.WebAPI.Database
{
    public static class DatabaseHelper
    {

        public static void SeedDatabase(this IServiceCollection services)
        {
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApiContext>();
                context.Database.EnsureCreated();
                context.Usuarios.AddRange(new[]
                {
                    new Usuario() { Id = 1, Nome="User", Email="user@mail.com", Senha="password" }
                });
                context.Tarefas.AddRange(new[]
                {
                    new Tarefa() { Id=1, DataConclusao = DateTime.Now, DataEntrega = DateTime.Now, Descricao = "Lorem" }
                });
                context.SaveChanges();
            }
        }
    }
}
