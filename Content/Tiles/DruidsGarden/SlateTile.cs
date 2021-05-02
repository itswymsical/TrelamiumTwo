using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Tiles.DruidsGarden
{
    public class SlateTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            dustType = 1;
            mineResist = 0.35f;
            soundType = SoundID.Tink;
            soundStyle = 2;
            drop = ModContent.ItemType<Items.Placeable.Slate>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Slate");
            AddMapEntry(new Color(75, 72, 72), name);
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}