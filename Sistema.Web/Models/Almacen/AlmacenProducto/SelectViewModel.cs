using System.Collections.Generic;
namespace Sistema.Web.Models.Almacen.AlmacenProducto
{
    public class SelectViewModel
    {
        public int ProId { get; set; }
        public string ProDescripcion { get; set; }
        public string ProIdentificacion { get; set; }
        public double AlmacenAguascalientes { get; set; }
        public double AlmacenGeneral { get; set; }
        public string SucNombre { get; set; }

        //existencias["Fred"] = 0;
    }
}