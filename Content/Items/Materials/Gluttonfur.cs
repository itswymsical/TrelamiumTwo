using Terraria;
using Terraria.ID;

namespace TrelamiumTwo.Content.Items.Materials
{
	public class Gluttonfur : TrelamiumItem
	{
		public override void SetDefaults()
		{
			item.width = item.height = 26;
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;
			item.value = Item.buyPrice(silver: 50);
		}
	}
}
