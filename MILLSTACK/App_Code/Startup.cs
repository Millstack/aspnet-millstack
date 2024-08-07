using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MILLSTACK.Startup))]
namespace MILLSTACK
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
