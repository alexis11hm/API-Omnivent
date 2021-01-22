using System;

namespace Sistema.Web.Models.Almacen.Almacen
{
    public class AlmacenViewModel
    {
        public int AlmId { get; set; }
        public string AlmDescripcion { get; set; }
        public char AlmEstatus { get; set; }
        public Int16 SucId { get; set; }

        public string sucursal { get; set; }
    }
}
