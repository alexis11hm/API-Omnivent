using System.Collections.Generic;

namespace Sistema.Web.Models.Almacen.AlmacenProducto
{
    public class ExistenciasPorAlmacenPorProductoViewModel
    {
        public int ProId { get; set; }
        public string productoDescripcion { get; set; }
        public string productoIdentificacion { get; set; }
        public string almacen { get; set; }
        public double AlpStockActual { get; set; }
        public string sucursal { get; set; }
    }

    public class ExistenciasViewModel
    {
        public int ProId { get; set; }
        public string productoDescripcion { get; set; }
        public string productoIdentificacion { get; set; }
        public double AlpStockActual { get; set; }
        public string sucursal { get; set; }
        public Dictionary<string,double> almacenes { get; set; }
    }
}