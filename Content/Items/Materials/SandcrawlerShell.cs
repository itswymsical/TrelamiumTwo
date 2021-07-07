using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Materials
{
	public class SandcrawlerShell : ModItem
	{
		public override string Texture => Assets.Items.Materials + "SandcrawlerShell";
		public override void SetDefaults()
		{
			item.width = item.height = 20;
			item.maxStack = 999;
			item.value = Item.sellPrice(silver: 4);
			item.rare = ItemRarityID.Blue;
		}
	}
}
