using Exiled.API.Features;

namespace ScpReroll
{
    public static class InputHandler
    {
        public static void HandleHKey(Player player)
        {
            if (player == null)
                return;

            Plugin.Instance.RerollManager.TryReroll(player);
        }
    }
}
