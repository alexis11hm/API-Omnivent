using System.Collections.Generic;
namespace Sistema.Web.Models.Almacen.AlmacenProducto
{
    public class SelectViewModel
    {
        public int ProId { get; set; }
        public string ProDescripcion { get; set; }
        public string ProIdentificacion { get; set; }
        public string ProCodigoBarras { get; set; }
        public Dictionary<string, string> existencias { get; set; }

        //existencias["Fred"] = 0;
    }
}