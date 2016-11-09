using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nba.Web.Startup))]
namespace Nba.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
