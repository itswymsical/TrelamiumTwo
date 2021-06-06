using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TrelamiumTwo.Content.Items.Placeable;

namespace TrelamiumTwo.Content.Walls
{
	public class CobblestoneWall : ModWall
	{
		public override void SetDefaults()
		{
			drop = ModContent.ItemType<CobblestoneWallItem>();
			dustType = 0;
			AddMapEntry(new Color(43, 43, 53));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}