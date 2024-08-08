using Exiled.API.Features;
using PlayerRoles;
using Respawning;

namespace hud_enhanced
{
    public class Tools
    {
        private static readonly Random Random = new Random();

        #region PlayerManager
        public void HighMoveState(Player ply)
        {
            PlayerManager playerManager = ply.GameObject.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                playerManager.moveState = 3;
            }
        }

        public void PhysicalMoveState(Player ply, PlayerRoles.FirstPersonControl.PlayerMovementState state)
        {
            PlayerManager playerManager = ply.GameObject.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                switch (state)
                {
                    case PlayerRoles.FirstPersonControl.PlayerMovementState.Sneaking:
                        playerManager.moveState = 1;
                        break;
                    case PlayerRoles.FirstPersonControl.PlayerMovementState.Walking:
                        playerManager.moveState = 2;
                        break;
                    case PlayerRoles.FirstPersonControl.PlayerMovementState.Sprinting:
                        playerManager.moveState = 3;
                        break;
                }
            }
        }

        public void NoClipState(Player ply, bool enabled)
        {
            PlayerManager playerManager = ply.GameObject.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                switch (enabled)
                {
                    case true:
                        playerManager.noclipState = "✨| NoClip Enabled";
                        break;
                    case false:
                        playerManager.noclipState = "✨| NoClip Disabled";
                        break;
                }
            }
        }

        public void IncrementKills(Player ply)
        {
            PlayerManager playerManager = ply.GameObject.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                playerManager.killCount++;
            }
        }
        #endregion

        #region Strings
        public string NoiseString(int moveState) => moveState switch
        {
            1 => "🔊| >",
            2 => "🔊| >>",
            3 => "🔊| >>>",
            _ => "🔊| ",
        };
        #endregion

        #region Formatting
        public string FormatRoleText(RoleTypeId roleType) => roleType switch
        {
            RoleTypeId.ClassD => "Class-D",
            RoleTypeId.Scientist => "Scientist",
            RoleTypeId.FacilityGuard => "Facility Guard",
            RoleTypeId.NtfPrivate => "NTF Private",
            RoleTypeId.NtfCaptain => "NTF Captain",
            RoleTypeId.NtfSergeant => "NTF Sergeant",
            RoleTypeId.NtfSpecialist => "NTF Specialist",
            RoleTypeId.ChaosConscript => "Chaos Conscript",
            RoleTypeId.ChaosRifleman => "Chaos Rifleman",
            RoleTypeId.ChaosRepressor => "Chaos Repressor",
            RoleTypeId.ChaosMarauder => "Chaos Marauder",
            RoleTypeId.Scp049 => "SCP-049",
            RoleTypeId.Scp0492 => "SCP-049-2",
            RoleTypeId.Scp079 => "SCP-079",
            RoleTypeId.Scp096 => "SCP-096",
            RoleTypeId.Scp106 => "SCP-106",
            RoleTypeId.Scp173 => "SCP-173",
            RoleTypeId.Scp3114 => "SCP-3114",
            RoleTypeId.Scp939 => "SCP-939",
            RoleTypeId.Tutorial => "Tutorial",
            RoleTypeId.Overwatch => "Overwatch",
            RoleTypeId.Filmmaker => "Film Maker",
            RoleTypeId.None => "None",
            _ => "None",
        };

        public string FormatTeamText(SpawnableTeamType team) => team switch
        {
            SpawnableTeamType.NineTailedFox => "Nine-Tailed Fox",
            SpawnableTeamType.ChaosInsurgency => "Chaos Insurgency",
            SpawnableTeamType.None => "Undecided",
            _ => "Undecided",
        };
        #endregion
    }
}