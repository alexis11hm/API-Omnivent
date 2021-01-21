using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema.Entidades.Almacen
{
    public class AlmacenProducto
    {
        [Column("alm_id")]
        public int AlmId { get; set; }
        [Column("pro_id")]
        public int ProId { get; set; }
        [Column("alp_stock_actual")]
        public float AlpStockActual { get; set; }

        public Almacen almacen { get; set; }

        public Producto producto { get; set; }
    }
}