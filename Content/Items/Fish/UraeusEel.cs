using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Fish
{
	public class UraeusEel : ModItem
	{
		public override string Texture => Assets.Items.Fish + "UraeusEel";
		public override void SetDefaults()
		{
			item.width = item.height = 22;
			item.maxStack = 999;
			item.value = Item.sellPrice(silver: 4, copper: 25);
			item.rare = ItemRarityID.Green;
		}
	}
}
