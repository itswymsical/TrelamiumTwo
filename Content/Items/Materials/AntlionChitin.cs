using Terraria;
using Terraria.ID;

namespace TrelamiumTwo.Content.Items.Materials
{
	public sealed class AntlionChitin : TrelamiumItem
	{
		public override void SetDefaults()
		{
			item.width = item.height = 20;
			item.maxStack = 999;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(0, 0, 0, 83);

			item.material = true;
		}
	}
}
