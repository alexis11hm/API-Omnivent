namespace Sistema.Web.Models.Ventas.VentaDetalle
{
    public class VentaDetalleViewModel
    {
        public int VedId { get; set; }
        public int VtaId { get; set; }
        public int ProId { get; set; }
        public decimal VedPrecio { get; set; }
        public decimal VedDescuento { get; set; }
        public double VedCantidad { get; set; }
        public string Venta { get; set; }
        public string Producto { get; set; }
    }
}
