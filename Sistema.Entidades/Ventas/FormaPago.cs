using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema.Entidades.Ventas
{

    public class FormaPago
    {
        [Column("fop_id")]
        public int FopId { get; set; }
        [Column("fop_descripcion ")]
        public string FopDescripcion { get; set; }
    }
}
