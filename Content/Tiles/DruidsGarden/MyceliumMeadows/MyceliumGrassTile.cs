using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Tiles.DruidsGarden.MyceliumMeadows
{
    public class MyceliumGrassTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            dustType = 39;
            mineResist = 0.35f;
            soundType = SoundID.Grass;
            soundStyle = 2;
            Main.tileMerge[Type][ModContent.TileType<SlateTile>()] = true;
            Main.tileMerge[Type][ModContent.TileType<AlluviumOreTile>()] = true;
            Main.tileMerge[Type][ModContent.TileType<AlderwoodTrunk>()] = true;
            AddMapEntry(new Color(100, 111, 59));
        }
        #region RandomUpdate, Dust, KillTile
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            {
                fail = true;
                Main.tile[i, j].type = (ushort)ModContent.TileType<LoamBlockTile>();
                WorldGen.SquareTileFrame(i, j, true);
                for (int i1 = 0; i1 < 3; i1++)
                {
                    Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, 39, 0f, 0f, 0, default, 1.0f);
                }
            }
        }
        #endregion
    }
}