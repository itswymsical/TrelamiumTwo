using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TrelamiumTwo.Utilities.Extensions;

namespace TrelamiumTwo.Content.Tiles.DustifiedCaverns
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
}
