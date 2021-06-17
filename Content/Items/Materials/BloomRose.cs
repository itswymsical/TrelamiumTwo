using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Materials
{
	public class BloomRose : ModItem
	{
		public override string Texture => Assets.Items.Materials + "BloomRose";
		public override void SetDefaults()
		{
			item.width = item.height = 20;
			item.maxStack = 999;

			item.rare = ItemRarityID.White;
			item.useStyle = ItemUseStyleID.EatingUsing;
		}
	}
}
