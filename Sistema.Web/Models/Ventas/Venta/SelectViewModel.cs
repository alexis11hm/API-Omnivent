using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Ventas.Venta
{
    public class SelectViewModel
    {
        public int VtaId { get; set; }
        public int VtaFolioVenta { get; set; }
        public DateTime VtaFecha { get; set; }
        public decimal VtaTotal { get; set; }
        public string VtaEstatus { get; set; }
        public string Sucursal { get; set; }
        public string Vendedor { get; set; }
        public string ListaPrecios { get; set; }
    }
}
