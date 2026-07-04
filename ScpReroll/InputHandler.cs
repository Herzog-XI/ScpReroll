using System;
using CommandSystem;
using Exiled.API.Features;

namespace ScpReroll
{
    public static class InputHandler
    {
        public static void TryActivate(Player player)
        {
            Plugin.Instance.RerollManager.TryReroll(player);
        }
    }

    [CommandHandler(typeof(ClientCommandHandler))]
    public class RerollCommand : ICommand
    {
        public string Command => "reroll";
        public string[] Aliases => new[] { "rerollscp", "scpreroll" };
        public string Description => "Reroll your SCP within the first seconds after spawning.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (player == null)
            {
                response = "Player not found.";
                return false;
            }

            bool success = Plugin.Instance.RerollManager.TryReroll(player);

            response = success ? "Rolling..." : "Could not reroll.";
            return success;
        }
    }
}
