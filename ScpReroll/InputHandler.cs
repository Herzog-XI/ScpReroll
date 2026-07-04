using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace ScpReroll
{
    public class InputHandler
    {
        public void OnUsingItem(UsingItemEventArgs ev)
        {
            // Temporary fallback input:
            // Pressing H directly may require a Harmony/input patch.
            // For now, reroll will be triggered through the client command in the next step.
        }

        public static void TryActivate(Player player)
        {
            Plugin.Instance.RerollManager.TryReroll(player);
        }
    }
}
