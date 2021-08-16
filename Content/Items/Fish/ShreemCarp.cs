using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Fish
{
	public class ShreemCarp : ModItem
	{
		public override string Texture => Assets.Items.Fish + "ShreemCarp";
		public override void SetDefaults()
		{
			Item.width = Item.height = 22;
			Item.maxStack = 999;
			Item.value = Item.sellPrice(silver: 2, copper: 50);
			Item.rare = ItemRarityID.Blue;
		}
	}
}
