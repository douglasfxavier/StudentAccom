using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentAccom.Startup))]
namespace StudentAccom
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
