using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Webinar.Startup))]
namespace Webinar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
