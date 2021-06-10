using Terraria;
using Terraria.ID;

namespace TrelamiumTwo.Content.Items.Materials
{
	public class ShattertailShard : ArchaeologicalItem
	{
		protected override void SafeSetDefaults()
		{
			item.value = Item.sellPrice(silver: 2);
			item.maxStack = 999;
			item.rare = ItemRarityID.Orange;
		}
	}
}
