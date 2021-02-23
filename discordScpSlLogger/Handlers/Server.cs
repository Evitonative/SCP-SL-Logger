using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Mirror;

namespace discordScpSlLogger.Handlers
{
    class Server
    {
        public void SendingRemoteAdminCommand(SendingRemoteAdminCommandEventArgs ev)
        {
            var args = "";
            foreach (var arg in ev.Arguments)
            {
                args = $"{args} {arg}";
            }

            var msg = Plugin.Instance.Config.RaLog
                .Replace("$user", ev.CommandSender.Nickname)
                .Replace("$id", ev.CommandSender.SenderId)
                .Replace("$cmd", ev.Name)
                .Replace("$args", args);

            Plugin.DiscordHook(Plugin.Instance.Config.RaUrl, msg);
        }

        private static int GetMethodHash(Type invokeClass, string methodName)
        {
            return invokeClass.FullName.GetStableHashCode() * 503 + methodName.GetStableHashCode();
        }

        public void WaitingForPlayers()
        {
            if(Plugin.Instance.Config.ShowInRaLogWfp) Plugin.DiscordHook(Plugin.Instance.Config.RaUrl, Plugin.Instance.Config.WaitingForPlayers);
            if(Plugin.Instance.Config.ShowInNormalLogWfp) Plugin.DiscordHook(Plugin.Instance.Config.NormalUrl, Plugin.Instance.Config.WaitingForPlayers);
        }
    }
}
