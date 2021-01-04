using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Almacen.ListaPrecioDetalle
{
    public class ActualizarViewModel
    {

        [Key]
        public int LipId { get; set; }
        [Key]
        public int ProId { get; set; }
        public decimal LipDetSinIva { get; set; }
        public decimal LipDetConIva { get; set; }
    }
}
