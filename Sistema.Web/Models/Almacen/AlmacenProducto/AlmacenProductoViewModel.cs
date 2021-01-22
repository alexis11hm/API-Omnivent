namespace Sistema.Web.Models.Almacen.AlmacenProducto
{
    public class AlmacenProductoViewModel
    {
        public int AlmId { get; set; }
        public int ProId { get; set; }
        public double AlpStockActual { get; set; }
        public string almacen { get; set; }
        public string producto { get; set; }
    }
}