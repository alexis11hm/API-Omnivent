using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Almacen;

namespace Sistema.Datos.Mapping.Almacen
{
    public class AlmacenProductoMap : IEntityTypeConfiguration<AlmacenProducto>
    {
        public void Configure(EntityTypeBuilder<AlmacenProducto> builder)
        {
            builder.ToTable("PDV_ALMACEN_PRODUCTO")
               .HasKey(almp => new { almp.AlmId, almp.ProId });
        }
    }
}