using Microsoft.EntityFrameworkCore;

namespace RestaurantMenuApi.Model
{
    public class RestaurantMenuDbContext : DbContext
    {
        public RestaurantMenuDbContext(DbContextOptions<RestaurantMenuDbContext> options) : base(options)
        {

        }

        public DbSet<RestaurantMenuItem> RestaurantMenuItems { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
