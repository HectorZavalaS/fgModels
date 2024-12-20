using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(fgModels.Startup))]
namespace fgModels
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
