#region Using directives

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Items.Fish
{
	public sealed class UraeusEel : TrelamiumItem
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Uraeus Eel");
        public override void SetDefaults()
		{
			item.maxStack = 999;
			item.rare = ItemRarityID.Green;
			item.value = Item.sellPrice(0, 0, 4, 25);
		}
	}
}
