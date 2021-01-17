using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Verzel.TaskManager.WebAPI.DTO
{
    public static class MapperRegister
    {
        public static void RegisterMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
