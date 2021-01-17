﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Verzel.TaskManager.WebAPI.Models;

namespace Verzel.TaskManager.WebAPI.Database
{
    public static class DatabaseHelper
    {

        public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApiContext>();
                context.Database.EnsureCreated();
                context.Usuarios.AddRange(new[]
                {
                    new Usuario() { Id = 1, Nome="User", Email="user@mail.com", Senha="password" }
                });
                context.SaveChanges();
            }

            return app;
        }
    }
}