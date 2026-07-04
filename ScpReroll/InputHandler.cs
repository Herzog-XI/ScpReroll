using System;
using CommandSystem;
using Exiled.API.Features;

namespace ScpReroll
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class RerollCommand : ICommand
    {
        public string Command => "reroll";
        public string[] Aliases => new string[] { "rerollscp", "scpreroll" };
        public string Description => "Reroll your SCP.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string logName = sender.LogName ?? string.Empty;
            Player foundPlayer = null;

            foreach (Player player in Player.List)
            {
                if (player == null)
                    continue;

                if (!string.IsNullOrEmpty(player.UserId) && logName.Contains(player.UserId.Replace("@steam", "")))
                {
                    foundPlayer = player;
                    break;
                }

                if (!string.IsNullOrEmpty(player.Nickname) && logName.Contains(player.Nickname))
                {
                    foundPlayer = player;
                    break;
                }
            }

            if (foundPlayer == null)
            {
                response = "Player not found: " + logName;
                return false;
            }

            bool success = Plugin.Instance.RerollManager.TryReroll(foundPlayer);

            response = success ? "Rerolling..." : "You cannot reroll.";
            return success;
        }
    }
}
