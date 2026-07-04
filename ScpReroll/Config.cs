using Exiled.API.Interfaces;

namespace ScpReroll
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public int RerollTime { get; set; } = 10;

        public string Keybind { get; set; } = "RightControl";

        public bool ShowCountdown { get; set; } = true;
    }
}
