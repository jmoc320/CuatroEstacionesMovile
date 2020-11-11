using CuatroEstaciones.Core.Models;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace CuatroEstaciones.Core.Services.EF {

    public class DatabaseContext : DbContext {

        public DatabaseContext() {
            Database.EnsureCreated();
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<TipoServicio> TipoServicios { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaServicio> VentaServicios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var dbPath = DependencyService.Get<IDatabaseService>().GetDatabasePath();
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}