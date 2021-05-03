using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Tiles
{
    public class AzumuthTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            dustType = 56;
            minPick = 60;
            mineResist = 0.25f;
            soundType = SoundID.Tink;
            soundStyle = 2;

            drop = ModContent.ItemType<Items.Placeable.Carbon>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Azumuth");
            AddMapEntry(new Color(55, 0, 55), name);
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}