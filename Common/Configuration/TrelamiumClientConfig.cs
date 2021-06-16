using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace TrelamiumTwo.Common.Configuration
{
    public class TrelamiumClientConfig : ModConfig
    {
        public static TrelamiumClientConfig Instance => ModContent.GetInstance<TrelamiumClientConfig>();

        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("Developer Settings")]

        [DefaultValue(false)]
        [Label("Debug Mode")]
        [Tooltip("Enables/disables debug mode.")]
        public bool Debug { get; set; }
    }
}
