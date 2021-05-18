#region Using directives

using Terraria;
using Terraria.ID;

#endregion

namespace TrelamiumTwo.Content.Items.Materials
{
	public sealed class VultureGizzard : ArchaeologicalItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A chunk of the stomach from a desert avian.");
		}
		protected override void SafeSetDefaults()
		{
			item.value = Item.buyPrice(silver: 50);
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;

			item.material = true;
		}
	}
}
