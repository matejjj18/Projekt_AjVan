using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AjVan.Startup))]
namespace AjVan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
