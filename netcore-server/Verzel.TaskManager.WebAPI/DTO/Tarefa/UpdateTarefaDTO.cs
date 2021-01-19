using System;
using System.ComponentModel.DataAnnotations;

namespace Verzel.TaskManager.WebAPI.DTO.Tarefa
{
    public class UpdateTarefaDTO : TarefaDTO
    {
        [Required(ErrorMessage = "O campo id não pode ser nulo")]
        [Range(minimum: 1, maximum: long.MaxValue, ErrorMessage = "ID deve ser maior do que zero")]
        public long Id { get; set; }
    }
}
