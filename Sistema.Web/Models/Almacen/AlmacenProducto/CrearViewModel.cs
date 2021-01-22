using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Almacen.AlmacenProducto
{
    public class CrearViewModel
    {

        public int AlmId { get; set; }
        public int ProId { get; set; }
        public double AlpStockActual { get; set; }
    }
}
