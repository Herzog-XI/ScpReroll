using System;
using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;

namespace ScpReroll
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class RerollCommand : ICommand
    {
        public string Command => "reroll";
        public string[] Aliases => new[] { "rerollscp", "scpreroll" };
        public string Description => "Reroll your SCP.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!(sender is PlayerCommandSender playerSender))
            {
                response = "Only players can use this command.";
                return false;
            }

            Player player = Player.Get(playerSender.ReferenceHub);

            if (player == null)
            {
                response = "Player not found.";
                return false;
            }

            bool success = Plugin.Instance.RerollManager.TryReroll(player);

            response = success ? "Rerolling..." : "You cannot reroll.";
            return success;
        }
    }
}
