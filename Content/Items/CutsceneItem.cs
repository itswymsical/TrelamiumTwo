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
            DisplayName.SetDefault("Cutscene Item");
            Tooltip.SetDefault("Used to load a cutscene.");
        }

        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.useTurn = true;

			item.width = item.height = 64;

            item.useTime = item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.SwingThrow;
        }

		public override bool UseItem(Player player)
		{
			CutsceneLoader.GetCutscene<Credits>().Visible = !CutsceneLoader.GetCutscene<Credits>().Visible;

			return true;
		}
	}
}
