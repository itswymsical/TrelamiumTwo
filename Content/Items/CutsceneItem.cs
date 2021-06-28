using TrelamiumTwo.Common.Configuration;
using TrelamiumTwo.Common.Cutscenes;
using TrelamiumTwo.Core;
using TrelamiumTwo.Core.Loaders;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items
{
	public class CutsceneItem : ModItem
    {
        public override string Texture => Assets.PlaceholderTexture;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Debug Item");
            Tooltip.SetDefault("Used to debug stuff. Only usable with debug mode on.");
        }

        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.useTurn = true;

            item.width = item.height = 64;

            item.useTime = item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.SwingThrow;
        }

        public override bool CanUseItem(Player player) => ModContent.GetInstance<TrelamiumConfig>().Debug;

        public override bool UseItem(Player player)
        {
            CutsceneLoader.GetCutscene<WorldOpenup>().Visible = !CutsceneLoader.GetCutscene<WorldOpenup>().Visible;

            return true;
        }
    }
}