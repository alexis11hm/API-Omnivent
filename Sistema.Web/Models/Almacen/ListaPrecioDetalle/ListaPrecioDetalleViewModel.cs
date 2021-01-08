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
      public int productoId { get; set; }
      public string productoDescripcion { get; set; }
      public string productoCodigoBarras { get; set; }
    }
}
