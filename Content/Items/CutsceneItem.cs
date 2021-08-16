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
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.width = Item.height = 64;

            Item.useTime = Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
        }

        public override bool CanUseItem(Player player) => ModContent.GetInstance<TrelamiumConfig>().Debug;

        public override bool? UseItem(Player player)
        {
            CutsceneLoader.GetCutscene<Credits>().Visible = !CutsceneLoader.GetCutscene<Credits>().Visible;

            return true;
        }
    }
}