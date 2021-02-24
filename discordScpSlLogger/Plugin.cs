using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features;

using Server = Exiled.Events.Handlers.Server;
using Player = Exiled.Events.Handlers.Player;

namespace discordScpSlLogger
{
    public class Plugin : Plugin<Config>
    {
        private static readonly Lazy<Plugin> LazyInstance = new Lazy<Plugin>(valueFactory: () => new Plugin());
        public static Plugin Instance => LazyInstance.Value;

        public override PluginPriority Priority { get; } = PluginPriority.Low;
        
        private Plugin() { }

        private Handlers.Server server;
        private Handlers.Player player;

        public override void OnEnabled()
        {
            server = new Handlers.Server();
            player = new Handlers.Player();

            Server.SendingRemoteAdminCommand += server.SendingRemoteAdminCommand;
            Server.WaitingForPlayers += server.WaitingForPlayers;

            Player.Joined += player.Joined;
            Player.Left += player.Left;
            Player.Kicked += player.Kicked;
            Player.Banned += player.Banned;
        }

        public override void OnDisabled()
        {
            Server.SendingRemoteAdminCommand -= server.SendingRemoteAdminCommand;
            Server.WaitingForPlayers -= server.WaitingForPlayers;

            Player.Joined -= player.Joined;
            Player.Left -= player.Left;
            Player.Kicked -= player.Kicked;
            Player.Banned -= player.Banned;

            server = null;
        }

        public static async void DiscordHook(string msg, bool isRa = false) //TODO: Ensure this will not throw an error when no ip specified
        {
            var url = isRa ? Instance.Config.RaUrl : Instance.Config.NormalUrl;
            try
            {
                await Http.Post(url, new System.Collections.Specialized.NameValueCollection()
                {
                    {
                        "content",
                        "`" + DateTime.Now.ToString(System.Globalization.CultureInfo.CurrentCulture) + "` " + msg
                    }
                });
            }
            catch
            {
                Log.Error("Discord Webhook post threw an error. Have you set the webhook url in XXXX-config.yml");
            }
        }
    }
}
