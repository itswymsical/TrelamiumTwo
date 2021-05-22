using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Walls
{
	public class LoamWall : ModWall
	{
		public override void SetDefaults() {
			dustType = 0;
			AddMapEntry(new Color(103, 67, 42));
		}

		public override void NumDust(int i, int j, bool fail, ref int num) {
			num = fail ? 1 : 3;
		}
	}
}