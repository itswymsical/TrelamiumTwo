#region Using directives

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Items.Materials
{
	public sealed class ScatteredFlower : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("It releases a charming scent");
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 40;
			item.maxStack = 999;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(0, 0, 3, 0);

			item.material = true;
		}
	}
}
