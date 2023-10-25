using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WEBBANDIENTHOAI.Startup))]
namespace WEBBANDIENTHOAI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
