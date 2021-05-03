using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Tiles
{
    public class StellarConduitPlatingTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            dustType = 226;
            soundType = SoundID.Tink;
            soundStyle = 2;
            drop = ModContent.ItemType<Items.Placeable.StellarConduitPlating>();
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Stellar Conduit Plating");
            AddMapEntry(new Color(21, 87, 255), name);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}