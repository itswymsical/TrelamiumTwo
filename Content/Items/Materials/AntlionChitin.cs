using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Materials
{
	public sealed class AntlionChitin : ModItem
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
