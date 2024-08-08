using UnityEngine;
using Exiled.API.Features;
using MEC;
using Player = Exiled.API.Features.Player;
using PlayerRoles;
using Exiled.API.Extensions;

namespace hud_enhanced
{
    public class PlayerManager : MonoBehaviour
    {
        Tools Tools = new Tools();

        public Player? ply;
        private CoroutineHandle hudDisplayCoroutineHandle;

        public int killCount { get; set; } = 0;
        public bool hudDisplay { get; set; } = true;
        public int moveState { get; set; } = 0;
        public string noclipState { get; set; } = "✨| NoClip Disabled";

        private void Awake()
        {
            this.ply = Player.Get(this.gameObject);

            if (ply != null)
            {
                hudDisplayCoroutineHandle = Timing.RunCoroutine(DisplayHud(ply));
            }
        }

        private void OnDestroy()
        {
            if (hudDisplayCoroutineHandle.IsRunning)
                Timing.KillCoroutines(hudDisplayCoroutineHandle);
        }

        private IEnumerator<float> DisplayHud(Player player)
        {
            while (true)
            {

                string noiseLevelLine = Tools.NoiseString(moveState);

                var uiColour = player.Role.Color.ToHex();
                var ntfColour = RoleTypeId.NtfSergeant.GetColor().ToHex();
                var chaosColour = RoleTypeId.ChaosMarauder.GetColor().ToHex();

                string playerName = player.Nickname;
                string playerRank = player.RankName;
                string playerRole = Tools.FormatRoleText(player.Role.Type);
                string nextTeam = Tools.FormatTeamText(Respawn.NextKnownTeam);
                int playerSpectators = player.CurrentSpectatingPlayers.ToList().Count;


                string nextTeamLine = $"👥| {nextTeam}";
                string roundTimerLine = $"⏰| {Round.ElapsedTime.Minutes}:{Round.ElapsedTime.Seconds:D2}";
                string ntfTicketsLine = $"🔴| {Math.Round(Respawn.NtfTickets)}";
                string chaosTicketsLine = $"🔴| {Math.Round(Respawn.ChaosTickets)}";
                string respawnTimerLine = $"⌛| {Math.Round(Respawn.TimeUntilSpawnWave.TotalSeconds)}";

                string playerNameLine = $"👤| {playerName}";
                string playerRankLine = $"💎| {playerRank}";
                string playerRoleLine = $"🎭| {playerRole}";
                string playerSpectatorsLine = $"👥| {playerSpectators}";
                string killCountLine = $"🔪| {killCount}";

                string combinedHumanHint = $"<color={uiColour}><size=25><align=left>" +
                                      $"<pos=-348><voffset=-375>{playerNameLine}</pos></voffset>\n" +
                                      $"<pos=-348>{playerRankLine}</pos>\n\n" +
                                      $"<pos=-348>{playerRoleLine}</pos>\n" +
                                      $"<pos=-348>{noiseLevelLine}</pos>\n" +
                                      $"<pos=-348>{playerSpectatorsLine}</pos>\n" +
                                      $"<pos=-348>{killCountLine}</pos>" +
                                      "</align></size></color>";

                string combinedTutorialHint = $"<color={uiColour}><size=25><align=left>" +
                                      $"<pos=-348><voffset=-375>{playerNameLine}</pos></voffset>\n" +
                                      $"<pos=-348>{playerRankLine}</pos>\n\n" +
                                      $"<pos=-348>{playerRoleLine}</pos>\n" +
                                      $"<pos=-348>{noclipState}</pos>\n" +
                                      $"<pos=-348>{playerSpectatorsLine}</pos>\n" +
                                      $"<pos=-348>{killCountLine}</pos>" +
                                      "</align></size></color>";

                string combinedScpHint = $"<color={uiColour}><size=25><align=left>" +
                                      $"<pos=-348><voffset=-330>{playerNameLine}</pos></voffset>\n" +
                                      $"<pos=-348>{playerRankLine}</pos>\n\n" +
                                      $"<pos=-348>{playerRoleLine}</pos>\n" +
                                      $"<pos=-348>{playerSpectatorsLine}</pos>\n" +
                                      $"<pos=-348>{killCountLine}</pos>" +
                                      "</align></size></color>";

                string combinedSpectatorHint = $"<size=25><align=left>" +
                                      $"<pos=-348><voffset=-550>{nextTeamLine}</pos></voffset>\n" +
                                      $"<pos=-348>{respawnTimerLine}</pos>\n" +
                                      $"<pos=-348>{roundTimerLine}</pos>\n\n" +
                                      $"<pos=-348><color={ntfColour}>{ntfTicketsLine}</color></pos>\n" +
                                      $"<pos=-348><color={chaosColour}>{chaosTicketsLine}</color></pos>" +
                                      "</align></size></color>";


                if (hudDisplay)
                {
                    if (player.IsHuman && !player.IsTutorial)
                    {
                        player.ShowHint($"{combinedHumanHint}", 5);
                    }
                    else if (player.IsTutorial)
                    {
                        player.ShowHint($"{combinedTutorialHint}", 5);
                    }
                    else if (player.IsScp)
                    {
                        player.ShowHint($"{combinedScpHint}", 5);
                    }
                    else if (player.IsDead)
                    {
                        player.ShowHint($"{combinedSpectatorHint}", 5);
                    }
                }

                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}