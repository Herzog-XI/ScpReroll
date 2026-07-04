using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;
using PlayerRoles;

namespace ScpReroll
{
    public class Config : IConfig
    {
        [Description("Enable or disable the plugin.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Enable debug messages.")]
        public bool Debug { get; set; } = false;

        [Description("How many seconds after spawning an SCP can reroll.")]
        public float RerollTime { get; set; } = 10f;

        [Description("How long the blackout animation lasts.")]
        public float AnimationTime { get; set; } = 1.2f;

        [Description("Only allow one reroll per round.")]
        public bool OneRerollPerRound { get; set; } = true;

        [Description("Command players use.")]
        public string CommandName { get; set; } = "reroll";

        [Description("Show reroll hint.")]
        public bool ShowHint { get; set; } = true;

        [Description("Allowed SCPs.")]
        public List<RoleTypeId> AllowedScps { get; set; } = new List<RoleTypeId>
        {
            RoleTypeId.Scp049,
            RoleTypeId.Scp079,
            RoleTypeId.Scp096,
            RoleTypeId.Scp106,
            RoleTypeId.Scp173,
            RoleTypeId.Scp939,
            RoleTypeId.Scp3114
        };
    }
}
