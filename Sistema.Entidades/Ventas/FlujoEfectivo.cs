using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Sistema.Entidades.Ventas
{

    public class FlujoEfectivo
    {
        [Column("fle_id")]
        public int FleId { get; set; }
        [Column("fle_fecha", TypeName = "datetime")]
        public DateTime FleFecha { get; set; }
        [Column("fle_importe", TypeName = "money")]
        public decimal FleImporte { get; set; }
        [Column("fop_id")]
        public int FopId { get; set; }
        [Column("fle_tipo")]
        public char FleTipo { get; set; }
        [Column("fle_referencia")]
        public string FleReferencia { get; set; }
        [Column("fle_observaciones")]
        public string FleObservaciones { get; set; }
        [Column("caj_descripcion")]
        public string FleDescripcion { get; set; }
        [Column("sucursal")]
        public string Sucursal { get; set; }
        [Column("cac_id")]
        public int CacId { get; set; }
        public FormaPago formaPago { get; set; }
    }
}
