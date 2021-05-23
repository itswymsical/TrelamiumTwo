using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Tiles.DustifiedCaverns
{
    public class DarkSandstoneTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][TileID.Sand] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            dustType = 268;
            mineResist = 0.75f;
            minPick = 60;
            soundType = SoundID.Tink;
            soundStyle = 2;        
            AddMapEntry(new Color(125, 40, 30));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}