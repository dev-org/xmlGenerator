using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XmlGenerator.Startup))]
namespace XmlGenerator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
