using Microsoft.EntityFrameworkCore;
using System;
using Verzel.TaskManager.WebAPI.Models;

namespace Verzel.TaskManager.WebAPI.Database
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) 
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
