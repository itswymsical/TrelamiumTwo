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
			item.width = item.height = 22;
			item.maxStack = 999;
			item.value = Item.sellPrice(silver: 3);
			item.rare = ItemRarityID.Green;
		}
	}
}
