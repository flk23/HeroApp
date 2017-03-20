using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Ninject;

//using Ninject;
//using Ninject.Parameters;
//using Ninject.Extensions.Conventions;
//using Ninject.Syntax;
//using Ninject.Web.Common;

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
