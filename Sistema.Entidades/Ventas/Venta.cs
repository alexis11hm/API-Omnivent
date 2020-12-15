using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Sistema.Entidades.Ventas
{

    public class Venta
    {
        [Column("vta_id")]
        public int VtaId { get; set; }
        [Column("vta_folio_venta")]
        public int VtaFolioVenta { get; set; }
        [Column("vta_fecha", TypeName = "datetime")]
        public DateTime VtaFecha { get; set; }
        [Column("vta_total", TypeName = "money")]
        public decimal VtaTotal { get; set; }
        [Required]
        [StringLength(1,ErrorMessage = "Solo debe haber un caracter")]
        [Column("vta_estatus")]
        public string VtaEstatus { get; set; }
        [Column("sucursal")]
        public string Sucursal { get; set; }
        [Column("vendedor")]
        public string Vendedor { get; set; }
        [Column("lista_precios")]
        public string ListaPrecios { get; set; }

    }
}
