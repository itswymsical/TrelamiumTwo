#region Using directives

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Items.Fish
{
	public sealed class Scaracod : TrelamiumItem
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Scaracod");
        public override void SetDefaults()
		{
			item.maxStack = 999;
			item.rare = ItemRarityID.Orange;
			item.value = Item.sellPrice(0, 0, 3, 50);
		}
	}
}
