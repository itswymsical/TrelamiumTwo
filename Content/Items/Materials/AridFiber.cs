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
			item.width = item.height = 20;
			item.maxStack = 999;
			item.value = Item.sellPrice(copper: 18);
			item.rare = ItemRarityID.White;
		}
	}
}
