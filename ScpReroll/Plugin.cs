using System;
using Exiled.API.Features;
using HarmonyLib;

namespace ScpReroll
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance { get; private set; }

        public override string Name => "ScpReroll";
        public override string Author => "Oliver";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 0, 0);

        public EventHandlers EventHandlers { get; private set; }
        public RerollManager RerollManager { get; private set; }

        private Harmony harmony;

        public override void OnEnabled()
        {
            Instance = this;

            RerollManager = new RerollManager();
            EventHandlers = new EventHandlers();

            EventHandlers.Register();

            harmony = new Harmony("oliver.scpreroll");
            harmony.PatchAll();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            harmony?.UnpatchAll("oliver.scpreroll");
            harmony = null;

            EventHandlers?.Unregister();

            EventHandlers = null;
            RerollManager = null;
            Instance = null;

            base.OnDisabled();
        }
    }
}
