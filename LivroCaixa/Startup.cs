using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LivroCaixa.Startup))]
namespace LivroCaixa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
