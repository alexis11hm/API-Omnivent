using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Sistema.Entidades.Almacen
{
    public class Producto
    {
        [Column("pro_id")]
        public int ProId { get; set; }
        [Column("pro_descripcion")]
        public string ProDescripcion { get; set; }
        [Column("pro_codigo_barras")]
        public string ProCodigoBarras { get; set; }
        [Column("pro_identificacion")]
        public string ProIdentificacion { get; set; }
        [Column("fam_descripcion")]
        public string Familia { get; set; }
        [Column("sub_descripcion")]
        public string SubFamilia { get; set; }
        [Column("pro_precio_general_iva", TypeName = "money")]
        public decimal ProPrecioGeneralIva { get; set; }
        [Column("pro_costo_general_iva", TypeName = "money")]
        public decimal ProCostoGeneralIva { get; set; }
    }
}