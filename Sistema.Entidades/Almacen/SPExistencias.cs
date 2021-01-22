using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema.Entidades.Almacen
{
    public class SPExistencias
    {
        [Column("pro_id")]
        public int ProId { get; set; }
        [Column("pro_descripcion")]
        public string ProDescripcion { get; set; }
        [Column("pro_identificacion")]
        public string ProIdentificacion { get; set; }
    }
}