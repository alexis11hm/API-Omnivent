using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Mapping.Ventas;
using Sistema.Datos.Mapping.Usuarios;
using Sistema.Datos.Mapping.Almacen;
using Sistema.Entidades.Ventas;
using Sistema.Entidades.Usuarios;
using Sistema.Entidades.Almacen;

namespace Sistema.Datos
{
    public class DbContextSistema : DbContext
    {
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ListaPrecio> ListaPrecios { get; set; }
        public DbSet<ListaPrecioDetalle> ListaPrecioDetalles { get; set; }

        public DbContextSistema(DbContextOptions<DbContextSistema> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new VentaMap());
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new ProductoMap());
            modelBuilder.ApplyConfiguration(new ListaPrecioMap());
            modelBuilder.ApplyConfiguration(new ListaPrecioDetalleMap());
        }

    }
}
