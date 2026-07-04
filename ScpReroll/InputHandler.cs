using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;

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
            Player player = Player.List.FirstOrDefault(p => p.UserId == sender.SenderId);

            if (player == null)
            {
                response = "Only players can use this command.";
                return false;
            }

            bool success = Plugin.Instance.RerollManager.TryReroll(player);

            response = success ? "Rerolling..." : "You cannot reroll.";
            return success;
        }
    }
}
