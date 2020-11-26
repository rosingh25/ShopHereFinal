using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopHere.Startup))]
namespace ShopHere
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
