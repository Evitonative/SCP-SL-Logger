using System.ComponentModel;
using Exiled.API.Interfaces;

namespace discordScpSlLogger
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public string RaUrl { get; set; } = "";
        public string NormalUrl { get; set; } = "";
        
        [Description("Waiting for Players")]
        public bool ShowInRaLogWfp { get; set; } = true;
        public bool ShowInNormalLogWfp { get; set; } = true;
        public string WaitingForPlayers { get; set; } = "🔰 Waiting for players...";

        [Description("RaLog")]
        public string RaLogMsg { get; set; } = ":notepad_spiral: $user($id) used RA command: $cmd $args";
    }
}
