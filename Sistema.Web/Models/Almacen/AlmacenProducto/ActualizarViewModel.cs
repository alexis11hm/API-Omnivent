using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Almacen.AlmacenProducto
{
    public class ActualizarViewModel
    {
        [Key]
        public int AlmId { get; set; }
        public int ProId { get; set; }
        public float AlpStockActual { get; set; }
    }
}
