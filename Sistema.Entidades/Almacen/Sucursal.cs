using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema.Entidades.Almacen
{
    public class Sucursal
    {
        [Column("suc_id")]
        public int SucId { get; set; }
        [Column("suc_nombre")]
        public string SucNombre { get; set; }
    }
}