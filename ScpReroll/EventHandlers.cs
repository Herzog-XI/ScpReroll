using Exiled.Events.Handlers;

namespace ScpReroll
{
    public class EventHandlers
    {
        public void Register()
        {
            Player.Spawned += OnPlayerSpawned;
            Player.Dying += OnPlayerDying;
            Server.RoundStarted += OnRoundStarted;
            Server.RestartingRound += OnRoundRestarting;
        }

        public void Unregister()
        {
            Player.Spawned -= OnPlayerSpawned;
            Player.Dying -= OnPlayerDying;
            Server.RoundStarted -= OnRoundStarted;
            Server.RestartingRound -= OnRoundRestarting;
        }

        private void OnPlayerSpawned(Exiled.Events.EventArgs.Player.SpawnedEventArgs ev)
        {
            Plugin.Instance.RerollManager.OnSpawn(ev.Player);
        }

        private void OnPlayerDying(Exiled.Events.EventArgs.Player.DyingEventArgs ev)
        {
            Plugin.Instance.RerollManager.OnDeath(ev.Player);
        }

        private void OnRoundStarted()
        {
            Plugin.Instance.RerollManager.OnRoundStarted();
        }

        private void OnRoundRestarting()
        {
            Plugin.Instance.RerollManager.OnRoundEnded();
        }
    }
}
