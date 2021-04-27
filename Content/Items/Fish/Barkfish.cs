#region Using directives

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Items.Fish
{
	public sealed class Barkfish : TrelamiumItem
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Barkfish");
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(0, 0, 1, 0);
		}
	}
}
