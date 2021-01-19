using System;

namespace Verzel.TaskManager.WebAPI.Models
{
    public class Tarefa : ModelBase
    {
        public string Descricao { get; set; }
        public DateTime DataEntrega { get; set; }
        public DateTime DataConclusao { get; set; }
    }
}
