using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Interconnected.Startup))]
namespace Interconnected
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
