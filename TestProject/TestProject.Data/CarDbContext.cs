using Microsoft.EntityFrameworkCore;
using TestProject.Data.Entities;

namespace TestProject.Data
{
    public class CarDbContext : DbContext
    {
        public CarDbContext()
        {
        }
        
        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options)
        {
        }

        public virtual DbSet<AreaEntity> Areas { get; set; }
        public virtual DbSet<CarCategoryEntity> CarCategories { get; set; }
        public virtual DbSet<GarageEntity> Garages { get; set; }
        public virtual DbSet<CarEntity> Cars { get; set; }
    }
}
