using Exiled.Events.EventArgs.Player;
using Exiled.Events.Handlers;
using PlayerRoles;

namespace ScpReroll
{
    public class EventHandlers
    {
        public void Register()
        {
            Player.ChangingRole += OnChangingRole;
            Player.Dying += OnDying;
            Server.RoundStarted += OnRoundStarted;
            Server.RestartingRound += OnRestartingRound;
        }

        public void Unregister()
        {
            Player.ChangingRole -= OnChangingRole;
            Player.Dying -= OnDying;
            Server.RoundStarted -= OnRoundStarted;
            Server.RestartingRound -= OnRestartingRound;
        }

        private void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player == null)
                return;

            if (!ev.IsAllowed)
                return;

            if (!Plugin.Instance.Config.AllowedScps.Contains(ev.NewRole))
                return;

            Plugin.Instance.RerollManager.OnScpAssigned(ev.Player, ev.NewRole);
        }

        private void OnDying(DyingEventArgs ev)
        {
            Plugin.Instance.RerollManager.OnDeath(ev.Player);
        }

        private void OnRoundStarted()
        {
            Plugin.Instance.RerollManager.Reset();
        }

        private void OnRestartingRound()
        {
            Plugin.Instance.RerollManager.Reset();
        }
    }
}
