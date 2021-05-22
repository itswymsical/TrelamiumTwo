using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Walls
{
	public class DarkSandstoneWall : ModWall
	{
		public override void SetDefaults()
		{
			dustType = 0;
			AddMapEntry(new Color(60, 30, 18));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}