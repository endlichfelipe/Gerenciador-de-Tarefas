using AutoMapper;
using Verzel.TaskManager.WebAPI.DTO.Tarefa;

namespace Verzel.TaskManager.WebAPI.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateTarefaDTO ,Models.Tarefa>();
            CreateMap<UpdateTarefaDTO, Models.Tarefa>();
        }
    }
}
