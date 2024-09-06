using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.EF
{
    public class Context : DbContext
    {
       public DbSet<Duyuru> Duyurular { get; set; }

        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Marka> Marka { get; set; }
        public DbSet<Model> Model { get; set; }
        public DbSet<Urun> Urun { get; set; }

        public DbSet<Kullanici> Kullanici{ get; set; }
        public DbSet<Musteri> Musteri{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Test;User Id=TestUser;Password=test123;TrustServerCertificate=True");
                base.OnConfiguring(optionsBuilder);
            }
        
    }
}
