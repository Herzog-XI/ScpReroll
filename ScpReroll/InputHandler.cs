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
            string logName = sender.LogName ?? string.Empty;

            Player player = Player.List.FirstOrDefault(p =>
                p != null &&
                (
                    (!string.IsNullOrEmpty(p.Nickname) && logName.Contains(p.Nickname)) ||
                    (!string.IsNullOrEmpty(p.UserId) && logName.Contains(p.UserId.Replace("@steam", "")))
                ));

            if (player == null)
            {
                response = "Player not found: " + logName;
                return false;
            }

            bool success = Plugin.Instance.RerollManager.TryReroll(player);

            response = success ? "Rerolling..." : "You cannot reroll.";
            return success;
        }
    }
}
