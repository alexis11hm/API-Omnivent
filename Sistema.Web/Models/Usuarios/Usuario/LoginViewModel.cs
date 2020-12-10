using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Usuarios.Usuario
{
    public class LoginViewModel
    {
        [Required]
        public string usuario { get; set; }
        [Required]
        public string password { get; set; }
    }
}
