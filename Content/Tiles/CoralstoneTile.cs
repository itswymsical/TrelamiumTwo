using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Tiles
{
    public class CoralstoneTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][TileID.Sand] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            dustType = 37;
            mineResist = 0.75f;
            soundType = SoundID.Tink;
            soundStyle = 2;
            drop = ModContent.ItemType<Items.Materials.Coralstone>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Coralstone");
            AddMapEntry(new Color(70, 64, 119), name);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}