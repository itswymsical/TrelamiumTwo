using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Cutscenes;
using TrelamiumTwo.Core;
using TrelamiumTwo.Core.Loaders;

namespace TrelamiumTwo.Content.Items
{
    public class CutsceneTool : TrelamiumItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cutscene Tool");
            Tooltip.SetDefault("Activates a test cutscene");
        }
        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 20;
            item.useTime = 20;
            item.UseSound = SoundID.Item81;
        }

        public override bool UseItem(Player player)
        {
            CutsceneLoader.GetCutscene<Credits>().Visible = !CutsceneLoader.GetCutscene<Credits>().Visible;

            return true;
        }
    }
}