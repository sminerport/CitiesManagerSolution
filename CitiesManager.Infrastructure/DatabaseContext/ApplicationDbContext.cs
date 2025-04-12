using CitiesManager.Core.Models;

using Microsoft.EntityFrameworkCore;

namespace CitiesManager.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public virtual DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().HasData(new City()
            {
                CityID = Guid.Parse("50AA4381-C47E-4D09-B751-E9BC9D8EFEA3"),
                CityName = "New York"
            });
            modelBuilder.Entity<City>().HasData(new City()
            {
                CityID = Guid.Parse("EBC9B5E2-C305-4437-9805-DF12274F2140"),
                CityName = "Paris"
            });
        }
    }
}