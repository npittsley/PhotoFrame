using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhotoFrame.Web.Startup))]
namespace PhotoFrame.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
