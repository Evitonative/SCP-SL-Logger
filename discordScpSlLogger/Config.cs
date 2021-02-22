using System.ComponentModel;
using Exiled.API.Interfaces;

namespace discordScpSlLogger
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public string RaUrl { get; set; } = "https://discord.com/api/webhooks/813130070675554374/Gr5q4KJPTNATEkGxUPosA267ZhiIZGF8VVvhS2ghuoq3g4rqGo_0LLYYHW0e3CeKsYhk";
        public string NormalUrl { get; set; } = "https://discord.com/api/webhooks/813135738207338506/Nmt4u3RcCO2rYeyr3bRVdc_P-kFjZev54EWDfOuzCWsruCRyK9s8YadKEHwovGp_99nq";
        
        [Description("Waiting for Players")]
        public bool ShowInRaLogWfp { get; set; } = true;
        public bool ShowInNormalLogWfp { get; set; } = true;
        public string WaitingForPlayers { get; set; } = "🔰 Waiting for players...";

        [Description("RaLog")]
        public string RaLogMsg { get; set; } = ":notepad_spiral: $user($id) used RA command: $cmd $args";
    }
}
