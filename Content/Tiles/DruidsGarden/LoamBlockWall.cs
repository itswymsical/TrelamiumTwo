using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace TrelamiumTwo.Content.Tiles.DruidsGarden
{
	public class LoamBlockWall : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[Type] = true;
			dustType = DustID.Dirt;
			AddMapEntry(new Color(103, 67, 42));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
			=> num = fail ? 1 : 3;


		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0.4f;
			g = 0.4f;
			b = 0.4f;
		}
	}
}