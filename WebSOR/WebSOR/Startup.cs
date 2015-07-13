using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebSOR.Startup))]
namespace WebSOR
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
