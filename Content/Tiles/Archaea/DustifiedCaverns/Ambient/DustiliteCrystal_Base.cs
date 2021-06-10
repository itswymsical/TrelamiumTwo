using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TrelamiumTwo.Helpers.Extensions;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Enums;

namespace TrelamiumTwo.Content.Tiles.Archaea.DustifiedCaverns.Ambient
{
	public abstract class DustiliteCrystal_Base : ModTile
	{
		public sealed override void SetDefaults()
		{
			Main.tileLighted[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileFrameImportant[Type] = true;

			TileID.Sets.CanBeClearedDuringGeneration[Type] = false;

			SafeSetDefaults();

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Dustilite Crystal");
			AddMapEntry(new Color(230, 200, 50), name);

			dustType = ModContent.DustType<Dusts.DustiliteCrystal>();
		}

		public override bool Drop(int i, int j)
		{
			if (Main.rand.NextBool(4))
			{
				Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<Items.Materials.DustiliteCrystal>());
			}
			return (false);
		}

		public virtual void SafeSetDefaults() { }

		public override void NumDust(int i, int j, bool fail, ref int num)
			=> num = 1;

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0.351f;
			g = 0.3f;
			b = 0.078f;
		}

		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
		{
			drawColor *= 0.6f;

			if (Main.rand.NextBool(400))
			{
				Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, ModContent.DustType<Dusts.DustiliteCrystal>());
			}
		}

		public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
		{
			spriteBatch.End();
			spriteBatch.Begin(default, BlendState.Additive, default, default, default, Filters.Scene["TrelamiumTwo:DustifiedCrystalShine"].GetShader().Shader);
			Filters.Scene["TrelamiumTwo:DustifiedCrystalShine"].GetShader().ApplyTime((float)Main.time * 0.02f).ApplyOpacity(0.8f);
			return true;
		}
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			spriteBatch.End();
			spriteBatch.Begin(default, default, default, default, default, default);
		}
	}
	public sealed class DustiliteCrystal_Large1 : DustiliteCrystal_Base
	{
		public override void SafeSetDefaults()
		{
			TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
			TileObjectData.newTile.Height = 5;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 18 };
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 2, 1);

			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceRight;

			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.addAlternate(1);

			TileObjectData.addTile(Type);
		}
	}
	public sealed class DustiliteCrystal_Large2 : DustiliteCrystal_Base
	{
		public override void SafeSetDefaults()
		{
			TileObjectData.newTile.CopyFrom(TileObjectData.Style5x4);
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 18 };
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 2, 1);

			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceRight;

			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 2, 2);
			TileObjectData.addAlternate(1);

			TileObjectData.addTile(Type);
		}
	}
	public sealed class DustiliteCrystal_Large3 : DustiliteCrystal_Base
	{
		public override void SafeSetDefaults()
		{
			TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 18 };
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 2, 0);

			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceRight;

			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 2, 2);
			TileObjectData.addAlternate(1);

			TileObjectData.addTile(Type);
		}
	}
	public sealed class DustiliteCrystal_Medium1 : DustiliteCrystal_Base
	{
		public override void SafeSetDefaults()
		{
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 18 };
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 3, 0);

			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceRight;

			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.addAlternate(1);

			TileObjectData.addTile(Type);
		}
	}
	public sealed class DustiliteCrystal_Medium2 : DustiliteCrystal_Base
	{
		public override void SafeSetDefaults()
		{
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 3, 0);

			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceRight;

			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.addAlternate(1);

			TileObjectData.addTile(Type);
		}
	}
	public sealed class DustiliteCrystal_Medium3 : DustiliteCrystal_Base
	{
		public override void SafeSetDefaults()
		{
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 3, 0);

			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceRight;

			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.addAlternate(1);

			TileObjectData.addTile(Type);
		}
	}
}
