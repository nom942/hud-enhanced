using CommandSystem;
using Exiled.API.Features;

namespace hud_enhanced.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class CmdConsoleHud : ICommand
    {
        public bool SanitizeResponse => false;

        public string Command { get; } = "hud";

        public string[]? Aliases { get; set; }

        public string Description { get; set; } = "usage: hud (hides custom UI elements)";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = $"ERROR: Failed to Execute {Command} command";
            try
            {
                Player ply = Player.Get(sender);

                PlayerManager playerManager = ply.GameObject.GetComponent<PlayerManager>();
                if (playerManager != null)
                {
                    if (playerManager.hudDisplay)
                    {
                        playerManager.hudDisplay = false;
                    }
                    else if (!playerManager.hudDisplay)
                    {
                        playerManager.hudDisplay = true;
                    }
                }

                response = $"Toggling HUD... Please wait a few seconds for the changes to take place.";
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}