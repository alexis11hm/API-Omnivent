using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema.Entidades.Almacen
{
    public class ListaPrecioDetalle
    {
        [Column("lip_id")]
        public int LipId { get; set; }
        [Column("pro_id")]
        public int ProId { get; set; }
        [Column("lpd_precio_sin_iva", TypeName = "money")]
        public decimal LipDetSinIva { get; set; }
        [Column("lpd_precio_con_iva", TypeName = "money")]
        public decimal LipDetConIva { get; set; }
        public ListaPrecio listaPrecio { get; set; }
        public Producto producto { get; set; }
    }
}