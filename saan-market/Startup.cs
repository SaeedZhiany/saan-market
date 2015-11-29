using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(saan_market.Startup))]
namespace saan_market
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
