using Terraria;
using Terraria.ID;

namespace TrelamiumTwo.Content.Items.Materials
{
	public sealed class Peony : ArchaeologicalItem
	{
		protected override void SafeSetDefaults()
		{
			item.value = Item.buyPrice(silver: 50);
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;
		}
	}
}
