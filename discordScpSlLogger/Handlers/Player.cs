using Exiled.API.Features;
using Exiled.Events.EventArgs;

namespace discordScpSlLogger.Handlers
{
    class Player
    {
        private readonly Config _config = Plugin.Instance.Config;
        
        public void Joined(JoinedEventArgs ev) //BUG: Ip not working, event broken
        {
            if (_config.PlayerJoined != "") 
                Plugin.DiscordHook(_config.PlayerJoined
                    .Replace("$Name", ev.Player.Nickname)
                    .Replace("$Steamid", ev.Player.UserId)
                    .Replace("$Ip", ev.Player.IPAddress)
                    .Replace("$Id", ev.Player.Id.ToString()));
        }

        public void Left(LeftEventArgs ev)
        {
            if (_config.PlayerLeft != "")
                Plugin.DiscordHook(_config.PlayerLeft
                    .Replace("$Name", ev.Player.Nickname)
                    .Replace("$Steamid", ev.Player.UserId)
                    .Replace("$Ip", ev.Player.IPAddress)
                    .Replace("$Id", ev.Player.Id.ToString()));
        }

        public void Kicked(KickedEventArgs ev)
        {
            if (_config.PlayerKicked != "")
                Plugin.DiscordHook(_config.PlayerKicked
                    .Replace("$Name", ev.Target.Nickname)
                    .Replace("$Steamid", ev.Target.UserId)
                    .Replace("$Ip", ev.Target.IPAddress)
                    .Replace("$Id", ev.Target.Id.ToString())
                    .Replace("$Reason", ev.Reason), 
                    _config.PlayerKickedInRa);
        }

        public void Banned(BannedEventArgs ev)
        {
            var banTime = (ev.Details.Expires - ev.Details.IssuanceTime / 100000).ToString(); //TODO: Setup calculation

            if (_config.PlayerBanned != "")
                Plugin.DiscordHook(_config.PlayerBanned
                        .Replace("$Name", ev.Target.Nickname)
                        .Replace("$SteamId", ev.Target.UserId)
                        .Replace("$Ip", ev.Target.IPAddress)
                        .Replace("$Id", ev.Target.Id.ToString())
                        .Replace("$Reason", ev.Details.Reason)
                        .Replace("$BanTime", banTime)
                        .Replace("$IssuerName", ev.Target.Nickname)
                        .Replace("$IssuerSteamId", ev.Target.UserId)
                        .Replace("$IssuerIp", ev.Target.IPAddress)
                        .Replace("$IssuerId", ev.Target.Id.ToString()),
                        _config.PlayerBannedInRa);
        }
    }
}
