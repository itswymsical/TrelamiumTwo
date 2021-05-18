#region Using directives

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Items.Materials
{
	public sealed class SandwormTooth : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 30;
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;
			item.value = Item.buyPrice(silver: 50);

			item.material = true;
		}
	}
}
