#region Using directives

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Items.Fish
{
	public sealed class Fleurer : TrelamiumItem
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Fleurer");
        public override void SetDefaults()
		{
			item.maxStack = 999;
			item.rare = ItemRarityID.Green;
			item.value = Item.sellPrice(0, 0, 3, 0);
		}
	}
}
