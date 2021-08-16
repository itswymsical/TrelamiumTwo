using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Materials
{
	public class HardenedCarapace : ModItem
	{
		public override string Texture => Assets.Items.Materials + "HardenedCarapace";
		public override void SetDefaults()
		{
			Item.width = Item.height = 20;
			Item.maxStack = 999;
			Item.value = Item.sellPrice(silver: 11);
			Item.rare = ItemRarityID.Blue;
		}
	}
}
