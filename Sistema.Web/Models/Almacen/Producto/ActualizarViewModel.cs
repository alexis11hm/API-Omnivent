using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Almacen.Producto
{
    public class ActualizarViewModel
    {
        [Key]
        public int ProId { get; set; }
        public string ProDescripcion { get; set; }
        public string ProCodigoBarras { get; set; }
        public string ProIdentificacion { get; set; }
        public string Familia { get; set; }
        public string SubFamilia { get; set; }
        public decimal ProPrecioGeneralIva { get; set; }
        public decimal ProCostoGeneralIva { get; set; }
    }
}
