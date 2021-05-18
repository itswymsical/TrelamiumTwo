#region Using directives

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Items.Materials
{
	internal sealed class HardenedCarapace : ModItem
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

			item.material = true;
		}
	}
}
