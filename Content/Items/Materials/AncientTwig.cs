#region Using directives

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Items.Materials
{
	public sealed class AncientTwig : ModItem
	{
		public override void SetDefaults()
		{
			item.width = item.height = 20;
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(0, 0, 2, 50);

			item.material = true;
		}
	}
}
