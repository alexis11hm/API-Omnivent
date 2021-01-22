using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Almacen.Sucursal
{
    public class CrearViewModel
    {
        [Key]
        public int SucId { get; set; }
        public string SucNombre { get; set; }
    }
}
