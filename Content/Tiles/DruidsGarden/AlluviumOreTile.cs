using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Tiles.DruidsGarden
{
    public class AlluviumOreTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            dustType = 151;
            mineResist = 0.35f;
            minPick = 50;
            soundType = SoundID.Tink;
            soundStyle = 2;
            drop = ModContent.ItemType<Items.Placeable.AlluviumOre>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Alluvium");
            AddMapEntry(new Color(81, 144, 37), name);
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}