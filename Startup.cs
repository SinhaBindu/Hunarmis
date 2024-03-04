using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hunarmis.Startup))]
namespace Hunarmis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
