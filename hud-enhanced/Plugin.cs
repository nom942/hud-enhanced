using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;
#pragma warning disable CS8602 

namespace hud_enhanced
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "hud-enhanced";
        public override string Author => "nom";
        public override Version Version => new Version(1, 2, 1);
        public static Plugin? Instance { get; private set; }

        private Handler? Handler;
        private Tools? Tools;

        public override void OnEnabled()
        {
            Handler = new Handler();
            Tools = new Tools();

            Log.Info("\n                 \n<(*)__ <(*)__ <(*)__\n (___/  (___/  (___/\n~-~-~~-~~-~~-~-~-~~-~");

            Player.Verified += Handler.AddPlayerManager;
            Player.Dying += Handler.DyingHud;
            Player.TogglingNoClip += Handler.TogglingNoClipHud;
            Player.ChangingRole += Handler.ChangingRoleHud;

            Player.ChangingMoveState += Handler.MoveStateNoise;
            Player.VoiceChatting += Handler.VoiceChattingNoise;
            Player.Shooting += Handler.ShootingNoise;

            Instance = this;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.Verified -= Handler.AddPlayerManager;
            Player.Dying -= Handler.DyingHud;
            Player.TogglingNoClip -= Handler.TogglingNoClipHud;
            Player.ChangingRole -= Handler.ChangingRoleHud;

            Player.ChangingMoveState -= Handler.MoveStateNoise;
            Player.VoiceChatting -= Handler.VoiceChattingNoise;
            Player.Shooting -= Handler.ShootingNoise;

            Handler = null;
            Tools = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}