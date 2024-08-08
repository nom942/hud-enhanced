using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace hud_enhanced
{
    public class Handler
    {
        Tools Tools = new Tools();

        private static readonly System.Random Random = new System.Random();

        #region PlayerManager
        public void AddPlayerManager(VerifiedEventArgs e)
        {
            e.Player.GameObject.AddComponent<PlayerManager>();
        }

        public void TogglingNoClipHud(TogglingNoClipEventArgs e)
        {
            Tools.NoClipState(e.Player, e.IsEnabled);
        }

        public void ChangingRoleHud(ChangingRoleEventArgs e)
        {
            if (e.NewRole == RoleTypeId.Tutorial)
            {
                Tools.NoClipState(e.Player, false);
            }
        }

        public void MoveStateNoise(ChangingMoveStateEventArgs e)
        {
            Tools.PhysicalMoveState(e.Player, e.NewState);
        }

        public void VoiceChattingNoise(VoiceChattingEventArgs e)
        {
            Tools.HighMoveState(e.Player);
        }

        public void ShootingNoise(ShootingEventArgs e)
        {
            Tools.HighMoveState(e.Player);
        }

        public void DyingHud(DyingEventArgs e)
        {
            if (e.Player == null || e.Attacker == null || e.Attacker == e.Player) return;

            Tools.IncrementKills(e.Player);
        }
        #endregion
    }
}