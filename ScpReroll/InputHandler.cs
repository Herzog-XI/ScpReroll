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

            string playerId = logName;

            int start = logName.IndexOf('(');
            int end = logName.IndexOf(')');

            if (start >= 0 && end > start)
                playerId = logName.Substring(start + 1, end - start - 1);

            Player player = Player.Get(playerId);

            if (player == null)
            {
                response = "Player not found: " + playerId;
                return false;
            }

            bool success = Plugin.Instance.RerollManager.TryReroll(player);

            response = success ? "Rerolling..." : "You cannot reroll.";
            return success;
        }
    }
}
