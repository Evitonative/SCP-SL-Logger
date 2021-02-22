using System;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features;

using Server = Exiled.Events.Handlers.Server;

namespace discordScpSlLogger
{
    public class Plugin : Plugin<Config>
    {
        private static readonly Lazy<Plugin> LazyInstance = new Lazy<Plugin>(valueFactory: () => new Plugin());
        public static Plugin Instance => LazyInstance.Value;

        public override PluginPriority Priority { get; } = PluginPriority.Low;
        
        private Plugin() { }

        private Handlers.Server server;

        public override void OnEnabled()
        {
            server = new Handlers.Server();

            Server.SendingRemoteAdminCommand += server.SendingRemoteAdminCommand;
            Server.WaitingForPlayers += server.WaitingForPlayers;
        }

        public override void OnDisabled()
        {
            Server.SendingRemoteAdminCommand -= server.SendingRemoteAdminCommand;
            Server.WaitingForPlayers -= server.WaitingForPlayers;

            server = null;
        }

        public static void DiscordHook(string url, string msg)
        {
            Http.Post(url, new System.Collections.Specialized.NameValueCollection()
            {
                {
                    "content",
                    "`" + DateTime.Now.ToString(System.Globalization.CultureInfo.CurrentCulture) + "` " + msg
                }
            });
        }
    }
}
