using Microsoft.EntityFrameworkCore;

namespace MoviessApi.Models
{
    public class DbContainer : DbContext
    {
        public DbContainer(DbContextOptions<DbContainer> options) :base(options)
        {

        }
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Movie> Movies { get; set; }




    }
}
