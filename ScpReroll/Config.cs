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

        [Description("Enable debug logging.")]
        public bool Debug { get; set; } = false;

        [Description("Seconds after spawning that reroll is allowed.")]
        public float RerollWindow { get; set; } = 10f;

        [Description("Length of the blackout/freeze animation.")]
        public float AnimationTime { get; set; } = 1.2f;

        [Description("Allow only one reroll per round.")]
        public bool OneRerollPerRound { get; set; } = true;

        [Description("Command shown to players.")]
        public string RerollCommand { get; set; } = ".reroll";

        [Description("Show reroll hint after spawning.")]
        public bool ShowHint { get; set; } = true;

        [Description("SCPs that may reroll.")]
        public List<RoleTypeId> AllowedScps { get; set; } = new()
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
