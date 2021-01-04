using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Almacen.ListaPrecioDetalle
{
    public class ListaPrecioDetalleViewModel
    {
      public int LipId { get; set; }
      public int ProId { get; set; }
      public decimal LipDetSinIva { get; set; }
      public decimal LipDetConIva { get; set; }
      public string listaPrecio { get; set; }
      public string producto { get; set; }
    }
}
