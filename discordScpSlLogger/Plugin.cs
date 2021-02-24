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
            
            //Server Events
            //Commands
            Server.SendingRemoteAdminCommand += server.SendingRemoteAdminCommand;
            //TODO: Server.SendingConsoleCommand += server.SendingConsoleCommand;
            //Round Events
            Server.WaitingForPlayers += server.WaitingForPlayers;
            //TODO: Server.RoundStarted += server.RoundStarted;
            //TODO: Server.EndingRound += server.RoundEnded;
            //TODO: Server.RespawningTeam += server.RespawningTeam;
            //Reporting
            //TODO: Server.LocalReporting += server.LocalReporting;
            //TODO: Server.ReportingCheater += server.ReportingCheater;

            //Player Connection and Punishments
            Player.Verified += player.Joined;
            Player.Destroying += player.Left;
            Player.Kicked += player.Kicked;
            Player.Banned += player.Banned;
            //TODO: Player.ChangingIntercomMuteStatus += player.ChangingIntercomMuteStatus;
            //TODO: Player.ChangingMuteStatus += player.ChangingMuteStatus;

            //General Stuff
            //TODO: Player.ActivatingWarheadPanel += player.ActivatingWarheadPanel;
            //TODO: Player.ActivatingWorkstation += player.ActivatingWorkstation;
            //TODO: Player.DeactivatingWorkstation += player.DeactivatingWorkstation;
            //TODO: Player.ChangingItem += player.ChangingItem;
            //TODO: .DroppingItem += player.DroppingItem;
            //TODO: Player.UsingMedicalItem += player.UsingMedicalItem;
            //TODO: Player.StoppingMedicalItem += player.StoppingMedicalItem;
            //TODO: Player.ReloadingWeapon += player.ReloadingWeapon;
            //TODO: Player.ChangingRole += player.ChangingRole;
            //TODO: Player.Died += player.Died;
            //TODO: Player.UnlockingGenerator += player.UnlockingGenerator;
            //TODO: Player.OpeningGenerator += player.OpeningGenerator;
            //TODO: Player.InsertingGeneratorTablet += player.InsertingGeneratorTablet;
            //TODO: Player.EjectingGeneratorTablet += player.EjectingGeneratorTablet;
            //TODO: Player.ClosingGenerator += player.ClosingGenerator;
            //TODO: Player.EjectingGeneratorTablet += player.EjectingGeneratorTablet;
            //TODO: Player.IntercomSpeaking += player.IntercomSpeaking;

            //Handcuffs
            //TODO: Player.Handcuffing += player.Handcuffing;
            //TODO: Player.RemovingHandcuffs += player.RemovingHandcuffs;

            //Interactions
            //TODO: Player.InteractingDoor += player.InteractingDoor;
            //TODO: Player.InteractingElevator += player.InteractingElevator;
            //TODO: Player.InteractingLocker += player.InteractingLocker;

            //106 Stuff
            //TODO: Player.EnteringFemurBreaker += player.EnteringFemurBreaker;
            //TODO: Player.EnteringPocketDimension += player.EnteringPocketDimension;
        }

        public override void OnDisabled()
        {
            //Server Events
            //Commands
            Server.SendingRemoteAdminCommand -= server.SendingRemoteAdminCommand;
            //TODO: Server.SendingConsoleCommand -= server.SendingConsoleCommand;
            //Round Events
            Server.WaitingForPlayers -= server.WaitingForPlayers;
            //TODO: Server.RoundStarted -= server.RoundStarted;
            //TODO: Server.EndingRound -= server.RoundEnded;
            //TODO: Server.RespawningTeam -= server.RespawningTeam;
            //Reporting
            //TODO: Server.LocalReporting -= server.LocalReporting;
            //TODO: Server.ReportingCheater -= server.ReportingCheater;

            //Player Connection and Punishments
            Player.Verified -= player.Joined;
            Player.Destroying -= player.Left;
            Player.Kicked -= player.Kicked;
            Player.Banned -= player.Banned;
            //TODO: Player.ChangingIntercomMuteStatus -= player.ChangingIntercomMuteStatus;
            //TODO: Player.ChangingMuteStatus -= player.ChangingMuteStatus;

            //General Stuff
            //TODO: Player.ActivatingWarheadPanel -= player.ActivatingWarheadPanel;
            //TODO: Player.ActivatingWorkstation -= player.ActivatingWorkstation;
            //TODO: Player.DeactivatingWorkstation -= player.DeactivatingWorkstation;
            //TODO: Player.ChangingItem -= player.ChangingItem;
            //TODO: .DroppingItem -= player.DroppingItem;
            //TODO: Player.UsingMedicalItem -= player.UsingMedicalItem;
            //TODO: Player.StoppingMedicalItem -= player.StoppingMedicalItem;
            //TODO: Player.ReloadingWeapon -= player.ReloadingWeapon;
            //TODO: Player.ChangingRole -= player.ChangingRole;
            //TODO: Player.Died -= player.Died;
            //TODO: Player.UnlockingGenerator -= player.UnlockingGenerator;
            //TODO: Player.OpeningGenerator -= player.OpeningGenerator;
            //TODO: Player.InsertingGeneratorTablet -= player.InsertingGeneratorTablet;
            //TODO: Player.EjectingGeneratorTablet -= player.EjectingGeneratorTablet;
            //TODO: Player.ClosingGenerator -= player.ClosingGenerator;
            //TODO: Player.EjectingGeneratorTablet -= player.EjectingGeneratorTablet;
            //TODO: Player.IntercomSpeaking -= player.IntercomSpeaking;

            //Handcuffs
            //TODO: Player.Handcuffing -= player.Handcuffing;
            //TODO: Player.RemovingHandcuffs -= player.RemovingHandcuffs;

            //Interactions
            //TODO: Player.InteractingDoor -= player.InteractingDoor;
            //TODO: Player.InteractingElevator -= player.InteractingElevator;
            //TODO: Player.InteractingLocker -= player.InteractingLocker;

            //106 Stuff
            //TODO: Player.EnteringFemurBreaker -= player.EnteringFemurBreaker;
            //TODO: Player.EnteringPocketDimension -= player.EnteringPocketDimension;

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
