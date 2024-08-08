using Exiled.API.Interfaces;
using System.ComponentModel;

namespace hud_enhanced
{
    public class Config : IConfig
    {
        [Description("Enable this plugin?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Enable debugging logs.")]
        public bool Debug { get; set; } = true;
    }
}