using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FitNotificaion2.Startup))]
namespace FitNotificaion2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
