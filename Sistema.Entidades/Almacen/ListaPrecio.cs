using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema.Entidades.Almacen
{
    public class ListaPrecio
    {
        [Column("lip_id")]
        public int LipId { get; set; }
        [Column("lip_nombre")]
        public string LipNombre { get; set; }
    }
}