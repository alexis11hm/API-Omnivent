using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Almacen;

namespace Sistema.Datos.Mapping.Almacen
{
    public class ListaPrecioDetalleMap : IEntityTypeConfiguration<ListaPrecioDetalle>
    {
        public void Configure(EntityTypeBuilder<ListaPrecioDetalle> builder)
        {
            builder.ToTable("PDV_LISTAP_DETALLE")
               .HasKey(lpd => lpd.ProId);
            builder.HasKey(lpd => lpd.LipId);
        }
    }
}