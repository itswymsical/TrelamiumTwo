using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace TrelamiumTwo.Content.Items.Materials
{
	public sealed class DustiliteCrystal : TrelamiumItem
	{
		public override void SetDefaults()
		{
			item.width = item.height = 20;
			item.maxStack = 999;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 2);
		}
    }
}
