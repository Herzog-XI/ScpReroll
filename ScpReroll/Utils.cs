using System.Linq;
using Exiled.API.Features;
using PlayerRoles;

namespace ScpReroll
{
    public static class Utils
    {
        public static bool IsRoleAlive(RoleTypeId role)
        {
            return Player.List.Any(player =>
                player != null &&
                player.IsAlive &&
                player.Role.Type == role);
        }

        public static string GetScpName(RoleTypeId role)
        {
            if (role == RoleTypeId.Scp049) return "SCP-049";
            if (role == RoleTypeId.Scp079) return "SCP-079";
            if (role == RoleTypeId.Scp096) return "SCP-096";
            if (role == RoleTypeId.Scp106) return "SCP-106";
            if (role == RoleTypeId.Scp173) return "SCP-173";
            if (role == RoleTypeId.Scp939) return "SCP-939";
            if (role == RoleTypeId.Scp3114) return "SCP-3114";

            return role.ToString();
        }
    }
}
