using System;
using System.ComponentModel.DataAnnotations;

namespace Verzel.TaskManager.WebAPI.DTO.Tarefa
{
    public abstract class TarefaDTO
    {
        [Required(ErrorMessage = "O campo descricao não pode ser nulo")]
        [MinLength(3, ErrorMessage = "A descrição deve conter no mínimo 3 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo dataEntrega não pode ser nulo")]
        [DataType(DataType.DateTime, ErrorMessage = "O campo dataEntrega deve estar num formato válido de data")]
        public DateTime DataEntrega { get; set; }

        [Required(ErrorMessage = "O campo dataConclusao não pode ser nulo")]
        [DataType(DataType.DateTime, ErrorMessage = "O campo dataConclusao deve estar num formato válido de data")]
        public DateTime DataConclusao { get; set; }
    }
}
