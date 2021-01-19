using System;
using System.Collections.Generic;
using Verzel.TaskManager.WebAPI.Database;
using Verzel.TaskManager.WebAPI.Models;

namespace Verzel.TaskManager.WebAPI.Test.Helpers
{
    public static class Utilities
    {
        public static void InitializeDbForTests(ApiContext db)
        {
            db.Database.EnsureCreated();
            db.Usuarios.Add(GetUsuario());
            db.Tarefas.AddRange(GetTarefas());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(ApiContext db)
        {
            db.Usuarios.RemoveRange(db.Usuarios);
            db.Tarefas.RemoveRange(db.Tarefas);
            InitializeDbForTests(db);
        }

        public static Usuario GetUsuario()
        {
            return new Usuario()
            {
                Id = 1,
                Email = "test@user.com",
                Nome = "Test User",
                Senha = "Test"
            };
        }

        public static IEnumerable<Tarefa> GetTarefas()
        {
            return new []
            {
                new Tarefa() { Id = 1, Descricao = "Tarefa 1", DataConclusao = DateTime.Now, DataEntrega = DateTime.Now },
                new Tarefa() { Id = 2, Descricao = "Tarefa 2", DataConclusao = DateTime.Now, DataEntrega = DateTime.Now },
            };
        }
    }
}
