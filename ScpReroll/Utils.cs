using System.Linq;
using Exiled.API.Features;
using PlayerRoles;

namespace ScpReroll
{
    public static class Utils
    {
        public static bool IsScp(RoleTypeId role)
        {
            return role is RoleTypeId.Scp049
                or RoleTypeId.Scp079
                or RoleTypeId.Scp096
                or RoleTypeId.Scp106
                or RoleTypeId.Scp173
                or RoleTypeId.Scp939
                or RoleTypeId.Scp3114;
        }

        public static bool IsRoleAlive(RoleTypeId role)
        {
            return Player.List.Any(player =>
                player != null &&
                player.IsAlive &&
                player.Role.Type == role);
        }

        public static string GetScpName(RoleTypeId role)
        {
            return role switch
            {
                RoleTypeId.Scp049 => "SCP-049",
                RoleTypeId.Scp079 => "SCP-079",
                RoleTypeId.Scp096 => "SCP-096",
                RoleTypeId.Scp106 => "SCP-106",
                RoleTypeId.Scp173 => "SCP-173",
                RoleTypeId.Scp939 => "SCP-939",
                RoleTypeId.Scp3114 => "SCP-3114",
                _ => role.ToString()
            };
        }
    }
}
