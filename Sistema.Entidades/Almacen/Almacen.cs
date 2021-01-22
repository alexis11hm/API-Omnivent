using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema.Entidades.Almacen
{
    public class Almacen
    {
        [Column("alm_id")]
        public int AlmId { get; set; }
        [Column("alm_descripcion")]
        public string AlmDescripcion { get; set; }
        [Column("alm_estatus")]
        public char AlmEstatus { get; set; }
        [Column("suc_id")]
        public Int16 SucId { get; set; }

        public Sucursal sucursal { get; set; }
    }
}