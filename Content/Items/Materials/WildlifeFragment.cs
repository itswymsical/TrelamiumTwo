using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Materials
{
	public class WildlifeFragment : ModItem
	{
		public override string Texture => Assets.Items.Materials + "WildlifeFragment";
		public override void SetDefaults()
		{
			Item.width = Item.height = 20;
			Item.maxStack = 999;
			Item.value = Item.sellPrice(copper: 25);
			Item.rare = ItemRarityID.Blue;
		}
        public override void AddRecipes()
        {
		}
    }
}
