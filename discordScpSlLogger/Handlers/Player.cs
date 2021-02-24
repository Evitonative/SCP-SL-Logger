﻿using Exiled.API.Features;
using Exiled.Events.EventArgs;

namespace discordScpSlLogger.Handlers
{
    class Player
    {
        private readonly Config _config = Plugin.Instance.Config;
        
        public void Joined(VerifiedEventArgs ev)
        {
            if (_config.PlayerJoined != "") 
                Plugin.DiscordHook(_config.PlayerJoined
                    .Replace("$Name", ev.Player.Nickname)
                    .Replace("$SteamId", ev.Player.UserId)
                    .Replace("$Ip", ev.Player.IPAddress)
                    .Replace("$Id", ev.Player.Id.ToString()));
        }

        public void Left(DestroyingEventArgs ev)
        {
            if (_config.PlayerLeft != "")
                Plugin.DiscordHook(_config.PlayerLeft
                    .Replace("$Name", ev.Player.Nickname)
                    .Replace("$SteamId", ev.Player.UserId)
                    .Replace("$Ip", ev.Player.IPAddress)
                    .Replace("$Id", ev.Player.Id.ToString()));
        }

        public void Kicked(KickedEventArgs ev)
        {
            if (ev.Reason.Contains("You have been banned.")) return;
            if (_config.PlayerKicked != "")
                Plugin.DiscordHook(_config.PlayerKicked
                    .Replace("$Name", ev.Target.Nickname)
                    .Replace("$SteamId", ev.Target.UserId)
                    .Replace("$Ip", ev.Target.IPAddress)
                    .Replace("$Id", ev.Target.Id.ToString())
                    .Replace("$Reason", ev.Reason), 
                    _config.PlayerKickedInRa);
        }

        public void Banned(BannedEventArgs ev)
        {
            var banTimeMin = (ev.Details.Expires - ev.Details.IssuanceTime)/10000000/60; //TODO: Setup calculation
            var banTimeHrs = 0;
            while (banTimeMin >= 60)
            {
                banTimeHrs += 1;
                banTimeMin -= 60;
            }

            var banTimeDays = 0;
            while (banTimeHrs >= 24)
            {
                banTimeDays += 1;
                banTimeHrs -= 24;
            }

            var banTime = _config.BanTimeFormat
                .Replace("$day", banTimeDays.ToString())
                .Replace("$hrs", banTimeHrs.ToString())
                .Replace("$min", banTimeMin.ToString());
            var reason = ev.Details.Reason == "" ? "`no reason specified`" : $"`{ev.Details.Reason}`";
            
            if (_config.PlayerBanned != "")
                Plugin.DiscordHook(_config.PlayerBanned
                        .Replace("$Name", ev.Target.Nickname)
                        .Replace("$SteamId", ev.Target.UserId)
                        .Replace("$Ip", ev.Target.IPAddress)
                        .Replace("$Id", ev.Target.Id.ToString())
                        .Replace("$Reason", reason)
                        .Replace("$BanTime", banTime)
                        .Replace("$IssuerName", ev.Target.Nickname)
                        .Replace("$IssuerSteamId", ev.Target.UserId)
                        .Replace("$IssuerIp", ev.Target.IPAddress)
                        .Replace("$IssuerId", ev.Target.Id.ToString()),
                        _config.PlayerBannedInRa);
        }
    }
}
