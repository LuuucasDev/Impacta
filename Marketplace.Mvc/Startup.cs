using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Marketplace.Mvc.Startup))]
namespace Marketplace.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
