using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.Events.EventArgs;

namespace discordScpSlLogger.Handlers
{
    class Server
    {
        public void SendingRemoteAdminCommand(SendingRemoteAdminCommandEventArgs ev)
        {
            string args = "";
            foreach (var arg in ev.Arguments)
            {
                args = $"{args} {arg}";
            }

            var msg = Plugin.Instance.Config.RaLogMsg
                .Replace("$user", ev.CommandSender.Nickname)
                .Replace("$id", ev.CommandSender.SenderId)
                .Replace("$cmd", ev.Name)
                .Replace("$args", args);
            
            Plugin.DiscordHook(Plugin.Instance.Config.RaUrl, msg);
        }

        public void WaitingForPlayers()
        {
            if(Plugin.Instance.Config.ShowInRaLogWfp) Plugin.DiscordHook(Plugin.Instance.Config.RaUrl, Plugin.Instance.Config.WaitingForPlayers);
            if(Plugin.Instance.Config.ShowInNormalLogWfp) Plugin.DiscordHook(Plugin.Instance.Config.NormalUrl, Plugin.Instance.Config.WaitingForPlayers);
        }
    }
}
