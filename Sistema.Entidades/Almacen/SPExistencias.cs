using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

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
        [Column("Almacén Aguascalientes")]
        public double AlmacenAguascalientes { get; set; }
        [Column("Almacen General")]
        public double AlmacenGeneral { get; set; }
        [Column("suc_nombre")]
        public string SucNombre { get; set; }
    }
}