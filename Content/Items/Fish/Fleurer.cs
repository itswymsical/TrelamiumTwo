using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Fish
{
	public class Fleurer : ModItem
	{
		public override string Texture => Assets.Items.Fish + "Fleurer";
		public override void SetDefaults()
		{
			Item.width = Item.height = 22;
			Item.maxStack = 999;
			Item.value = Item.sellPrice(silver: 3);
			Item.rare = ItemRarityID.Green;
		}
	}
}
