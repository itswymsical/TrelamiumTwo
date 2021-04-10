using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Tiles
{
    public class CarbonTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            dustType = 54;
            mineResist = 0.65f;
            soundType = SoundID.Tink;
            soundStyle = 2;

            drop = ModContent.ItemType<Items.Placeable.Carbon>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Carbon");
            AddMapEntry(new Color(55, 0, 55), name);
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}