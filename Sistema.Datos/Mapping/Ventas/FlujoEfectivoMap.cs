using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Ventas;

namespace Sistema.Datos.Mapping.Ventas
{
    public class FlujoEfectivoMap : IEntityTypeConfiguration<FlujoEfectivo>
    {
        public void Configure(EntityTypeBuilder<FlujoEfectivo> builder)
        {
            builder.ToTable("PDV_FLUJO_EFECTIVO")
               .HasKey(fle => fle.FleId);
        }
    }
}
