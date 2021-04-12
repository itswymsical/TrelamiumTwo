using Terraria;
using Terraria.ID;

namespace TrelamiumTwo.Content.Items.Materials
{
	public sealed class EntsTwig : TrelamiumItem
	{
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(copper: 20);

			item.material = true;
		}
	}
}