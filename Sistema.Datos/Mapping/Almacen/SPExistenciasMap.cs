using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Almacen;

namespace Sistema.Datos.Mapping.Almacen
{
    public class SPExistenciasMap : IEntityTypeConfiguration<SPExistencias>
    {
        public void Configure(EntityTypeBuilder<SPExistencias> builder)
        {
            builder.HasKey(ex => ex.ProId);
        }
    }
}