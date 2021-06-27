using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Fish
{
	public class MushOWar : ModItem
	{
		public override string Texture => Assets.Items.Fish + "MushOWar";
		public override void SetDefaults()
		{
			item.width = item.height = 22;
			item.maxStack = 999;
			item.value = Item.sellPrice(silver: 1, copper: 50);
			item.rare = ItemRarityID.White;
		}
	}
}
