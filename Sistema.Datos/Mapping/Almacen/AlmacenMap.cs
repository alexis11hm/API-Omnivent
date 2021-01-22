using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sistema.Datos.Mapping.Almacen
{
    public class AlmacenMap : IEntityTypeConfiguration<Sistema.Entidades.Almacen.Almacen>
    {
        public void Configure(EntityTypeBuilder<Sistema.Entidades.Almacen.Almacen> builder)
        {
            builder.ToTable("PDV_ALMACEN")
               .HasKey(alm => alm.AlmId);
        }
    }
}