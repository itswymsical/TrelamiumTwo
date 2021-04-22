#region Using directives

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Items.Fish
{
	public sealed class ShreemCarp : TrelamiumItem
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Shreem Carp");
        public override void SetDefaults()
		{
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(0, 0, 2, 50);
		}
	}
}
