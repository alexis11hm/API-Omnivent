﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Ventas.Venta
{
    public class VentaViewModel
    {
        public int VtaId { get; set; }
        public int VtaFolioVenta { get; set; }
        public string VtaFecha { get; set; }
        public decimal VtaTotal { get; set; }
        public string VtaEstatus { get; set; }
        public short SucId { get; set; }
        public int? VndId { get; set; }
        public int? LipId { get; set; }
    }
}
