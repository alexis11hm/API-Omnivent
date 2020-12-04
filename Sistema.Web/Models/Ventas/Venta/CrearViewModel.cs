using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Ventas.Venta
{
    public class CrearViewModel
    {
        [Key]
        public int VtaId { get; set; }
        public int VtaFolioVenta { get; set; }
        public DateTime VtaFecha { get; set; }
        public decimal VtaTotal { get; set; }
        [Required]
        [StringLength(1)]
        public string VtaEstatus { get; set; }
        public short SucId { get; set; }
        public int? VndId { get; set; }
        public int? LipId { get; set; }

    }
}
