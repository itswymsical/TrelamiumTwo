using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace TrelamiumTwo.Content.Tiles.DruidsGarden.Ambient
{
	public enum FoliageType : byte
	{
		Grass,
		Grass2,
		Grass3,
		Grass4,
		Grass5,
		Flower,
		Flower2,
		Mushroom,
		Flower3,
		Flower4,
		Flower5,
		Flower6
	}
	public class DGFoliageTile : ModTile
	{
		private const int FrameWidth = 18;
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoFail[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				ModContent.TileType<LoamTile>(),
				ModContent.TileType<LoamTile_Grass>()
			};
			TileObjectData.addTile(Type);
			AddMapEntry(new Color(90, 160, 40));
		}
		public override void NumDust(int i, int j, bool fail, ref int num)
			=> num = fail ? 1 : 3;
		public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
		{
			if (i % 2 == 1)
				spriteEffects = SpriteEffects.FlipHorizontally;
		}
		public override void RandomUpdate(int i, int j)
		{
			Tile tile = Framing.GetTileSafely(i, j);
			FoliageType stage = GetFoliageType(i, j);
			if (stage != FoliageType.Flower6)
			{
				if (Main.rand.Next(6) == 0)
				{
					tile.frameX += FrameWidth;
					if (Main.netMode != NetmodeID.SinglePlayer)
						NetMessage.SendTileSquare(-1, i, j, 1);
				}
			}
		}
		private FoliageType GetFoliageType(int i, int j)
		{
			Tile tile = Framing.GetTileSafely(i, j);
			return (FoliageType)(tile.frameX / FrameWidth);
		}
	}
}