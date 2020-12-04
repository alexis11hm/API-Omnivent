using System.ComponentModel.DataAnnotations;
using System;

namespace Sistema.Web.Models.Ventas.Venta
{
    public class ActualizarViewModel
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
