using Terraria;
using Terraria.ID;

namespace TrelamiumTwo.Content.Items.Materials
{
	public class HardenedCarapace : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A chunk from the rigid shell of an antlion.");
		}
		public override void SetDefaults()
		{
			item.width = item.height = 26;
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;
			item.value = Item.buyPrice(silver: 50);
		}
	}
}
