using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Ventas;

namespace Sistema.Datos.Mapping.Ventas
{
    public class VentaMap : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.ToTable("PDV_VENTA")
               .HasKey(v => v.VtaId);
            builder.Property(v => v.VtaFolioVenta)
                .HasMaxLength(50);
            builder.Property(v => v.VtaFecha);
            builder.Property(v => v.VtaTotal);
            builder.Property(v => v.VtaEstatus);
            builder.Property(v => v.Sucursal);
            builder.Property(v => v.Vendedor);
            builder.Property(v => v.ListaPrecios);
        }
    }
}
