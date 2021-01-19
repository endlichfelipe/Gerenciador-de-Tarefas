using Verzel.TaskManager.WebAPI.Models;

namespace Verzel.TaskManager.WebAPI.Authentication
{
    public interface ITokenService
    {
        string GenerateToken(Usuario usuario);
    }
}
