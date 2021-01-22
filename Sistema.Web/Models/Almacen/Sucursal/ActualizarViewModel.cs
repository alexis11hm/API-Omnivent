using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Almacen.Sucursal
{
    public class ActualizarViewModel
    {
        [Key]
        public int SucId { get; set; }
        public string SucNombre { get; set; }
    }
}
