using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Materials
{
	public class AridFiber : ModItem
	{
		public override string Texture => Assets.Items.Materials + "AridFiber";
		public override void SetDefaults()
		{
			Item.width = Item.height = 20;
			Item.maxStack = 999;
			Item.value = Item.sellPrice(copper: 18);
			Item.rare = ItemRarityID.White;
		}
	}
}
