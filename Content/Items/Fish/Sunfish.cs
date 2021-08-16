using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Fish
{
	public class Sunfish : ModItem
	{
		public override string Texture => Assets.Items.Fish + "Sunfish";
		public override void SetDefaults()
		{
			Item.width = Item.height = 22;
			Item.maxStack = 999;
			Item.value = Item.sellPrice(silver: 1, copper: 75);
			Item.rare = ItemRarityID.Green;
		}
	}
}
