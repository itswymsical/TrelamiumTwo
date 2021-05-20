using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Tiles.DruidsGarden
{
    public class AlderwoodTrunk : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            soundType = 0;
            Main.tileMerge[Type][ModContent.TileType<SlateTile>()] = true;
            Main.tileMerge[Type][ModContent.TileType<AlluviumOreTile>()] = true;
            Main.tileMerge[Type][ModContent.TileType<LoamTile>()] = true;

            AddMapEntry(new Color(244, 235, 66));
        }
    }
}