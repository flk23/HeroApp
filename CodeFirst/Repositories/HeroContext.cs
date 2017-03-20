using System.Data.Entity;
using CodeFirst.Models;

namespace CodeFirst.Repositories
{
    public class HeroContext: DbContext
    {
        //указываем подключение, что и для логинов/паролей
        public HeroContext() : base("DefaultConnection")
        { }

        public DbSet<Power> Powers { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
