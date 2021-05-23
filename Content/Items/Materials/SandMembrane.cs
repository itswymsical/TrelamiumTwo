#region Using directives

using Terraria;
using Terraria.ID;

#endregion

namespace TrelamiumTwo.Content.Items.Materials
{
	public sealed class SandMembrane : ArchaeologicalItem
	{
		protected override void SafeSetDefaults()
		{
			item.value = Item.buyPrice(silver: 50);
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;

			item.material = true;
		}
	}
}
