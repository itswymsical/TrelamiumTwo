using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace TrelamiumTwo.Content.Tiles.DruidsGarden.Ambient
{
    public sealed class AlluviumCluster : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Origin = new Point16(0, 1);

            dustType = 151;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Alluvium");
            AddMapEntry(new Color(81, 144, 37), name);
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
            => num = fail ? 1 : 3;

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.rand.Next(300) == 0)
            {
                int Index = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, 151, 0f, 0f, 0, default, 1.4f);
                Main.dust[Index].scale = 1.15f;
                Main.dust[Index].noGravity = true;
                Main.dust[Index].velocity *= 0f;
            }

            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);

            if (Main.drawToScreen)
                zero = Vector2.Zero;

            Texture2D glowmask = ModContent.GetTexture(Main.tileTexture[tile.type] + "_Glow");
            int height = tile.frameY == 18 ? 18 : 16;
            Main.spriteBatch.Draw(glowmask,
                new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero,
                new Rectangle(tile.frameX, tile.frameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.081f;
            g = 0.144f;
            b = 0.037f;
        }
    }
    public sealed class AlluviumClusterAlt : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Origin = new Point16(0, 1);

            dustType = 151;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Alluvium");
            AddMapEntry(new Color(81, 144, 37), name);
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
            => num = fail ? 1 : 3;
        
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (Main.rand.Next(300) == 0)
            {
                int Index = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, 151, 0f, 0f, 0, default, 1.4f);
                Main.dust[Index].scale = 1.15f;
                Main.dust[Index].noGravity = true;
                Main.dust[Index].velocity *= 0f;
            }

            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);

            if (Main.drawToScreen)
                zero = Vector2.Zero;

            Texture2D glowmask = ModContent.GetTexture(Main.tileTexture[tile.type] + "_Glow");
            int height = tile.frameY == 18 ? 18 : 16;
            Main.spriteBatch.Draw(glowmask,
                new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero,
                new Rectangle(tile.frameX, tile.frameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.081f;
            g = 0.144f;
            b = 0.037f;
        }
    }
}