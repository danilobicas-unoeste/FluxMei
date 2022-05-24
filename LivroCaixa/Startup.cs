using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LivroCaixa.Startup))]
namespace LivroCaixa
{
    public class FirebaseSettings
    {
        [JsonPropertyName("project_id")]
        public string ProjectId => "that-rug-really-tied-the-room-together-72daa";

        [JsonPropertyName("private_key_id")]
        public string PrivateKeyId => "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

        // ... and so on
    }

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
