using System;
using System.Linq;
using System.Reflection;
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
            string senderId = GetSenderId(sender);

            Player player = Player.List.FirstOrDefault(p => p.UserId == senderId);

            if (player == null)
            {
                response = "Only players can use this command.";
                return false;
            }

            bool success = Plugin.Instance.RerollManager.TryReroll(player);

            response = success ? "Rerolling..." : "You cannot reroll.";
            return success;
        }

        private static string GetSenderId(ICommandSender sender)
        {
            PropertyInfo senderIdProperty = sender.GetType().GetProperty("SenderId");
            if (senderIdProperty != null)
                return senderIdProperty.GetValue(sender) as string;

            PropertyInfo userIdProperty = sender.GetType().GetProperty("UserId");
            if (userIdProperty != null)
                return userIdProperty.GetValue(sender) as string;

            return null;
        }
    }
}
