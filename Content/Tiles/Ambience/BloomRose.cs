using TrelamiumTwo.Content.Dusts;
using TrelamiumTwo.Core;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace TrelamiumTwo.Content.Tiles.Ambience
{
	public class BloomRose : ModTile
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = Assets.Tiles.Ambience + "BloomRose";

			return mod.Properties.Autoload;
		}
		public override void SetDefaults()
		{
			Main.tileNoFail[Type] = true;
			Main.tileSpelunker[Type] = true;
			Main.tileLavaDeath[Type] = true;
			Main.tileFrameImportant[Type] = true;
			soundType = SoundID.Grass;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.Origin = Point16.Zero;

			TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.EmptyTile, 1, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 0);

			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.UsesCustomCanPlace = true;

			TileObjectData.newTile.DrawYOffset = -10;
			TileObjectData.newTile.CoordinateWidth = 22;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.CoordinateHeights = new int[] { 34 };

			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				TileID.Grass
			};

			TileObjectData.newTile.AnchorAlternateTiles = new int[]
			{
				TileID.ClayPot,
				TileID.PlanterBox
			};

			TileObjectData.addTile(Type);

			animationFrameHeight = 36;

			dustType = ModContent.DustType<PinkPetal>();
		}

		public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
		{
			if (i % 2 == 1)
				spriteEffects = SpriteEffects.FlipHorizontally;
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Framing.GetTileSafely(i, j);

			if (Main.rand.NextBool(10))
			{
				if (Main.dayTime)
				{
					if (tile.frameY > 0 * (short)animationFrameHeight)
						tile.frameY -= (short)animationFrameHeight;
				}
				else
				{
					if (tile.frameY < 3 * (short)animationFrameHeight)
						tile.frameY += (short)animationFrameHeight;
				}
			}
		}

		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
		{
			Tile tile = Framing.GetTileSafely(i, j);

			if (tile.frameY >= 1 * (short)animationFrameHeight)
				return;

			if (Main.rand.NextBool(500))
				Dust.NewDustDirect(new Vector2(i, j) * 16f, 0, 0, ModContent.DustType<Dusts.BloomRose>());
		}

		public override bool Drop(int i, int j)
		{
			Tile tile = Framing.GetTileSafely(i, j);

			if (tile.frameY >= 1 * (short)animationFrameHeight)
				return false;

			Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<Items.Materials.BloomRose>(), Main.rand.Next(1, 2));

			return false;
		}
	}
}