using System.ComponentModel.DataAnnotations;

namespace Verzel.TaskManager.WebAPI.DTO.Login
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O campo email não pode ser nulo")]
        [DataType(DataType.EmailAddress, ErrorMessage = "O campo email deve ter um formato válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo senha não pode ser nulo")]
        [MinLength(3, ErrorMessage = "A senha deve conter no mínimo 3 caracteres.")]
        public string Senha { get; set; }
    }
}
