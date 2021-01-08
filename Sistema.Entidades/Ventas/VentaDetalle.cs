using System.ComponentModel.DataAnnotations.Schema;
using Sistema.Entidades.Almacen;

namespace Sistema.Entidades.Ventas
{

    public class VentaDetalle
    {
        [Column("ved_id")]
        public int VedId { get; set; }
        [Column("vta_id")]
        public int VtaId { get; set; }
        [Column("pro_id")]
        public int ProId { get; set; }
        [Column("ved_precio_con_iva", TypeName = "money")]
        public decimal VedPrecio { get; set; }
        [Column("ved_importe_descuento", TypeName = "money")]
        public decimal VedDescuento { get; set; }
        [Column("ved_cantidad")]
        public float VedCantidad { get; set; }
        public Venta venta { get; set; }
        public Producto producto { get; set; }
    }
}
