using Terraria;
using Terraria.ID;

namespace TrelamiumTwo.Content.Items.Materials
{
	public class AncientTwig : TrelamiumItem
	{
		public override void SetDefaults()
		{
			item.width = item.height = 20;
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(0, 0, 2, 50);
		}
	}
}
