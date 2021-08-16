using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Materials
{
	public class DesolateHusk : ModItem
	{
		public override string Texture => Assets.Items.Materials + "DesolateHusk";
		public override void SetDefaults()
		{
			Item.width = Item.height = 20;
			Item.maxStack = 999;
			Item.value = Item.sellPrice(copper: 1);
			Item.rare = ItemRarityID.White;
		}
	}
}
