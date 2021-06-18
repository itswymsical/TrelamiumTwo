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
			item.width = item.height = 22;
			item.maxStack = 999;
			item.value = Item.sellPrice(silver: 1, copper: 75);
			item.rare = ItemRarityID.Green;
		}
	}
}
