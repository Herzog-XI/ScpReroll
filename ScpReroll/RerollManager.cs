using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;

namespace ScpReroll
{
    public class RerollManager
    {
        private readonly Dictionary<Player, DateTime> spawnTimes = new();
        private readonly HashSet<Player> usedReroll = new();
        private readonly HashSet<Player> rollingPlayers = new();

        public void Reset()
        {
            spawnTimes.Clear();
            usedReroll.Clear();
            rollingPlayers.Clear();
        }

        public void OnScpAssigned(Player player, RoleTypeId role)
        {
            if (player == null)
                return;

            if (rollingPlayers.Contains(player))
                return;

            if (!Plugin.Instance.Config.AllowedScps.Contains(role))
                return;

            spawnTimes[player] = DateTime.Now;

            if (Plugin.Instance.Config.ShowHint)
            {
                player.ShowHint(
                    $"Type <b>.{Plugin.Instance.Config.CommandName}</b> to reroll your SCP.\nYou can bind it in console.\n{Plugin.Instance.Config.RerollTime}s available.",
                    Plugin.Instance.Config.RerollTime
                );
            }
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

            if (secondsSinceSpawn > Plugin.Instance.Config.RerollTime)
            {
                player.ShowHint("Reroll expired.", 2f);
                return false;
            }

            if (Plugin.Instance.Config.OneRerollPerRound && usedReroll.Contains(player))
            {
                player.ShowHint("You already used your reroll.", 2f);
                return false;
            }

            List<RoleTypeId> options = Plugin.Instance.Config.AllowedScps
                .Where(role => role != player.Role.Type)
                .Where(role => !Utils.IsRoleAlive(role))
                .ToList();

            if (options.Count == 0)
            {
                player.ShowHint("No SCP available to reroll into.", 3f);
                return false;
            }

            RoleTypeId newRole = options[new Random().Next(options.Count)];

            usedReroll.Add(player);
            rollingPlayers.Add(player);

            player.ShowHint("<b>Rolling...</b>", 2f);

            player.EnableEffect(EffectType.Blinded, 255, Plugin.Instance.Config.AnimationTime);
            player.EnableEffect(EffectType.Ensnared, 255, Plugin.Instance.Config.AnimationTime);

            player.Role.Set(newRole, SpawnReason.ForceClass, RoleSpawnFlags.All);

            rollingPlayers.Remove(player);
            spawnTimes.Remove(player);

            player.ShowHint($"<b>You rerolled into {Utils.GetScpName(newRole)}</b>", 3f);

            return true;
        }
    }
}
