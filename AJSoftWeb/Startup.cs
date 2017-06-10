using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AJSoftWeb.Startup))]
namespace AJSoftWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
