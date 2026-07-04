using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using PlayerRoles;
using PlayerRoles.RoleAssign;

namespace ScpReroll
{
    public class RerollManager
    {
        private readonly Dictionary<Player, DateTime> spawnTimes = new();
        private readonly HashSet<Player> usedReroll = new();
        private readonly HashSet<Player> rollingPlayers = new();

        public void OnRoundStarted()
        {
            spawnTimes.Clear();
            usedReroll.Clear();
            rollingPlayers.Clear();
        }

        public void OnRoundEnded()
        {
            spawnTimes.Clear();
            usedReroll.Clear();
            rollingPlayers.Clear();
        }

        public void OnSpawn(Player player)
        {
            if (player == null || !Plugin.Instance.Config.AllowedScps.Contains(player.Role.Type))
                return;

            spawnTimes[player] = DateTime.Now;
            Timing.RunCoroutine(CountdownHint(player));
        }

        public void OnDeath(Player player)
        {
            if (player == null)
                return;

            spawnTimes.Remove(player);
            rollingPlayers.Remove(player);
        }

        public bool TryReroll(Player player)
        {
            if (player == null)
                return false;

            if (rollingPlayers.Contains(player))
                return false;

            if (!Plugin.Instance.Config.AllowedScps.Contains(player.Role.Type))
            {
                player.ShowHint("You are not allowed to reroll.", 2f);
                return false;
            }

            if (!spawnTimes.ContainsKey(player))
            {
                player.ShowHint("You cannot reroll now.", 2f);
                return false;
            }

            double secondsSinceSpawn = (DateTime.Now - spawnTimes[player]).TotalSeconds;

            if (secondsSinceSpawn > Plugin.Instance.Config.RerollWindow)
            {
                player.ShowHint("Reroll expired.", 2f);
                return false;
            }

            if (Plugin.Instance.Config.OneRerollPerRound && usedReroll.Contains(player))
            {
                player.ShowHint("You already used your reroll.", 2f);
                return false;
            }

            List<RoleTypeId> options = GetAvailableScps(player.Role.Type);

            if (options.Count == 0)
            {
                player.ShowHint("No SCP available to reroll into.", 3f);
                return false;
            }

            RoleTypeId newRole = options[UnityEngine.Random.Range(0, options.Count)];

            usedReroll.Add(player);
            rollingPlayers.Add(player);

            Timing.RunCoroutine(RerollRoutine(player, newRole));
            return true;
        }

        private List<RoleTypeId> GetAvailableScps(RoleTypeId currentRole)
        {
            return Plugin.Instance.Config.AllowedScps
                .Where(role => role != currentRole)
                .Where(role => !Utils.IsRoleAlive(role))
                .ToList();
        }

        private IEnumerator<float> RerollRoutine(Player player, RoleTypeId newRole)
        {
            player.ShowHint("<b>Rolling...</b>", 2f);

            player.EnableEffect(EffectType.Blinded, 255, Plugin.Instance.Config.AnimationTime);
            player.EnableEffect(EffectType.Ensnared, 255, Plugin.Instance.Config.AnimationTime);

            yield return Timing.WaitForSeconds(Plugin.Instance.Config.AnimationTime);

            player.Role.Set(newRole, SpawnReason.ForceClass, RoleSpawnFlags.All);

            yield return Timing.WaitForSeconds(0.4f);

            player.DisableEffect(EffectType.Ensnared);
            player.DisableEffect(EffectType.Blinded);

            rollingPlayers.Remove(player);
            spawnTimes.Remove(player);

            player.ShowHint($"<b>You rerolled into {Utils.GetScpName(newRole)}</b>", 3f);
        }

        private IEnumerator<float> CountdownHint(Player player)
        {
            while (player != null &&
                   player.IsAlive &&
                   Plugin.Instance.Config.AllowedScps.Contains(player.Role.Type) &&
                   spawnTimes.ContainsKey(player))
            {
                double elapsed = (DateTime.Now - spawnTimes[player]).TotalSeconds;
                int remaining = (int)Math.Ceiling(Plugin.Instance.Config.RerollWindow - elapsed);

                if (remaining <= 0)
                    break;

                if (!rollingPlayers.Contains(player) &&
                    !(Plugin.Instance.Config.OneRerollPerRound && usedReroll.Contains(player)))
                {
                    player.ShowHint(
                        $"Type <b>.reroll</b> to reroll your SCP.\nYou can bind it in console.\n{remaining}s remaining.",
                        1.1f
                    );
                }

                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}
