using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Almacen.ListaPrecioDetalle
{
    public class ActualizarViewModel
    {
        public int NumLip { get; set; }
        public int LipId { get; set; }
        public int ProId { get; set; }
        public decimal LipDetSinIva { get; set; }
        public decimal LipDetConIva { get; set; }
    }
}
