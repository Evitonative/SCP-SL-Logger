using System.ComponentModel;
using Exiled.API.Interfaces;

namespace discordScpSlLogger
{
    public class Config : IConfig
    {
        [Description("Note: To disable a value set it empty. Do NOT leave the URLs empty. Disable the RAlog feature by settings it to false.")]
        public bool IsEnabled { get; set; } = true;
        public string RaUrl { get; set; } = "";
        public string NormalUrl { get; set; } = "";

        [Description("RaLog")]
        public bool RaLogEnabled { get; set; } = true;
        public string RaLog { get; set; } = ":notepad_spiral: $user($steamid) used RA command: $cmd $args | Permitted: $allowed";

        [Description("Waiting for Players")]
        public string WaitingForPlayers { get; set; } = ":beginner: Waiting for players...";

        [Description("Join, leave, kick, ban")]
        public string PlayerJoined { get; set; } = ":arrow_forward: $Name($Steamid | $Id | ||$Ip||) joined.";
        public string PlayerLeft { get; set; } = ":arrow_backward: $Name($Steamid | $Id | ||$Ip||) left.";
        public bool PlayerKickedInRa { get; set; } = false;
        public string PlayerKicked { get; set; } = ":no_entry: $Name($Steamid | $id | ||$Ip||) has been kicked for `$reason`.";
        public bool PlayerBannedInRa { get; set; } = false;
        public string PlayerBanned { get; set; } = ":name_badge: $Name($Steamid | $id | ||$Ip||) has been banned by $IssuerName($IssuerSteamId | $IssuerId | ||$IssuerIp||) for $Reason for $BanTime";
    }
}
