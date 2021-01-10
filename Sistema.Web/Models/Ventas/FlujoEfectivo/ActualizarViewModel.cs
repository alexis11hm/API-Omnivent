using System;

namespace Sistema.Web.Models.Ventas.FlujoEfectivo
{
    public class ActualizarViewModel
    {
        public int FleId { get; set; }
        public DateTime FleFecha { get; set; }
        public decimal FleImporte { get; set; }
        public int FopId { get; set; }
        public char FleTipo { get; set; }
        public string FleReferencia { get; set; }
        public string FleObservaciones { get; set; }
        public string FleDescripcion { get; set; }
        public string Sucursal { get; set; }
        public int CacId { get; set; }
    }
}