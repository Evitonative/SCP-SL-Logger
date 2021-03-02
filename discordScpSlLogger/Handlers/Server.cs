using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Mirror;

namespace discordScpSlLogger.Handlers
{
    class Server
    {
        private readonly Config _config = Plugin.Instance.Config;
        public void SendingRemoteAdminCommand(SendingRemoteAdminCommandEventArgs ev)
        {
            if (!_config.RaLogEnabled) return;
            var args = "";
            foreach (var arg in ev.Arguments)
            {
                args = $"{args} {arg}";
            }

            var msg = Plugin.Instance.Config.RaLog
                .Replace("$user", ev.CommandSender.Nickname)
                .Replace("$steamid", ev.CommandSender.SenderId)
                .Replace("$cmd", ev.Name)
                .Replace("$args", args)
                .Replace("$allowed", ev.IsAllowed.ToString());

            Plugin.DiscordHook(msg, true);
        }

        public void SendingConsoleCommand(SendingConsoleCommandEventArgs ev)
        {
            var args = "";
            foreach (var arg in ev.Arguments)
            {
                args = $"{args} {arg}";
            }
            
            var msg = Plugin.Instance.Config.RaLog
                .Replace("$user", ev.Player.Nickname)
                .Replace("$steamid", ev.Player.UserId)
                .Replace("$cmd", ev.Name)
                .Replace("$args", args)
                .Replace("$allowed", ev.IsAllowed.ToString());
        }

        public void WaitingForPlayers()
        {
            if(_config.WaitingForPlayers != "") Plugin.DiscordHook(Plugin.Instance.Config.WaitingForPlayers);
        }
    }
}
