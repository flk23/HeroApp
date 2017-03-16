using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CodeFirst
{
    public class HeroContext: DbContext
    {
        //указываем строку подключения, что и для логинов/паролей
        public HeroContext() : base("DefaultConnection")
        { }

        public DbSet<Power> Powers { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Image> Images { get; set; }
    }

    public class PowerRepository
    {
        private readonly HeroContext _db;
        public PowerRepository(HeroContext context)
        {
            _db = context; 
        }

        public void Add(Power p)
        {
            _db.Powers.Add(p);
            _db.SaveChanges();
        }

        public void Remove(Power p)
        {
            _db.Powers.Remove(p);
            _db.SaveChanges();
        }

        public List<Power> List()
        {
            return _db.Powers.ToList();
        }

        public Power Get(Guid id)
        {
            return _db.Powers.Find(id);
        }
    }

    public class HeroRepository
    {
        private readonly HeroContext _db;

        public HeroRepository(HeroContext context)
        {
            _db = context;
        }

        public void Add(Hero h)
        {
            _db.Heroes.Add(h);
            _db.SaveChanges();
        }

        public void Remove(Hero h)
        {
            _db.Heroes.Remove(h);
            _db.SaveChanges();
        }

        public List<Hero> List()
        {
            return _db.Heroes.ToList();
        }

        public Hero Get(Guid id)
        {
            return _db.Heroes.Find(id);
        }
    }

    public class ImageRepository
    {
        private readonly HeroContext _db;

        public ImageRepository(HeroContext context)
        {
            _db = context;
        }

        public void Add(Image img)
        {
            _db.Images.Add(img);
            _db.SaveChanges();
        }
    }
}
