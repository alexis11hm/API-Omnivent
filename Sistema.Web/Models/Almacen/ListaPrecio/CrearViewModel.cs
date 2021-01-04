using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Almacen.ListaPrecio
{
    public class CrearViewModel
    {
        [Key]
        public int LipId { get; set; }
        public string LipNombre { get; set; }
    }
}
