#region Using directives

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Items.Fish
{
	public sealed class MushOWarItem : TrelamiumItem
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Mush O' War");
        public override void SetDefaults()
		{
			item.maxStack = 999;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(0, 0, 1, 50);
		}
	}
}
