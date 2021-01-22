using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Almacen.Almacen
{
    public class CrearViewModel
    {
        [Key]
        public int AlmId { get; set; }
        public string AlmDescripcion { get; set; }
        public char AlmEstatus { get; set; }
        public int SucId { get; set; }
    }
}