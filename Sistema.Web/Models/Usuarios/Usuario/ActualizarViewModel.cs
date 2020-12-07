using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Usuarios.Usuario
{
    public class ActualizarViewModel
    {
        [Required]
        public int idusuario { get; set; }
        [Required]
        public int idrol { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El usuario no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string usuario { get; set; }
        [Required]
        public string password { get; set; }
        public bool act_password { get; set; }
    }
}
