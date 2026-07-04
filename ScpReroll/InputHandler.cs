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
            Log.Info("[ScpReroll] Command used.");
            Log.Info("[ScpReroll] Sender LogName: " + sender.LogName);
            Log.Info("[ScpReroll] Sender Type: " + sender.GetType().FullName);

            Player player = Player.List.FirstOrDefault(p => p.Nickname == sender.LogName);

            if (player == null)
            {
                response = "Player not found. Server saw you as: " + sender.LogName;
                Log.Warn("[ScpReroll] Player not found.");
                return false;
            }

            bool success = Plugin.Instance.RerollManager.TryReroll(player);

            response = success ? "Rerolling..." : "You cannot reroll.";
            return success;
        }
    }
}
