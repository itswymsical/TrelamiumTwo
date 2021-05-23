using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace TrelamiumTwo.Core
{
    public class TrelamiumConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        // Visual Settings
        [Header("Visual Settings [i:75]")]

        [DefaultValue("Disabled")]
        [OptionStrings(new string[] { "Disabled", "Low", "Medium", "High" } )]
        [Label("Low Dust Mode")]
        [Tooltip("Changes the amount of dust that will be on the screen.")]
        public string LowDustMode;
        
        // Developer Settings
        [Header("Developer Settings [i:3611]")]

        [DefaultValue(false)]
        [Label("Debug Mode")]
        [Tooltip("Enables/Disables debug mode. Shows important information for testing purposes.")]
        public bool Debug;        
    }
}