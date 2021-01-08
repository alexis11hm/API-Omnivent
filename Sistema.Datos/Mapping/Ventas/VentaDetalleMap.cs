using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Ventas;

namespace Sistema.Datos.Mapping.Ventas
{
    public class VentaDetalleMap : IEntityTypeConfiguration<VentaDetalle>
    {
        public void Configure(EntityTypeBuilder<VentaDetalle> builder)
        {
            builder.ToTable("PDV_VENTA_DETALLE")
               .HasKey(v => v.VedId);
        }
    }
}
