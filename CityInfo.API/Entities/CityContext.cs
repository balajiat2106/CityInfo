using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Entities
{
    public class CityContext:DbContext
    {
        public CityContext(DbContextOptions<CityContext> options):base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<PointsOfInterest> PointsOfInterest { get; set; }
    }
}