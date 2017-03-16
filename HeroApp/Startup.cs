using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HeroApp.Startup))]
namespace HeroApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
