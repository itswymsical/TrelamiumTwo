using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Materials
{
	public class AntlionChitin : ModItem
	{
		public override string Texture => Assets.Items.Materials + "AntlionChitin";
		public override void SetDefaults()
		{
			Item.width = Item.height = 20;
			Item.maxStack = 999;
			Item.value = Item.sellPrice(copper: 16);
			Item.rare = ItemRarityID.White;
		}
	}
}
