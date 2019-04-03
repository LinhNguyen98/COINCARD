using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(COINCARD.Startup))]
namespace COINCARD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
