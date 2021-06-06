using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Materials
{
	public class Wheat : TrelamiumItem
	{
		public override void SetDefaults()
		{
			item.width = item.height = 20;
			item.maxStack = 999;
			item.value = Item.sellPrice(copper: 18);
			item.rare = ItemRarityID.Blue;
		}
	}
}
