using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Content.Tiles.DruidsGarden.Ambient;

namespace TrelamiumTwo.Content.Tiles.DruidsGarden
{
    public class LoamTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            dustType = 0;
            mineResist = 0.35f;
            drop = ModContent.ItemType<Items.Placeable.Loam>();

            soundType = SoundID.Dig;
            soundStyle = 2;
            #region tileMerge Fields
            Main.tileMerge[Type]
                [ModContent.TileType<SlateTile>()] = true;
            Main.tileMerge[Type]
                [ModContent.TileType<AlluviumOreTile>()] = true;
            Main.tileMerge[Type][ModContent.TileType<LoamTileGrass>()] = true;
            #endregion
            AddMapEntry(new Color(125, 85, 50));
        }
        public override void RandomUpdate(int i, int j)
        {
            if (WorldGen.genRand.Next(3) == 0)
                WorldGen.SpreadGrass(i, j, ModContent.TileType<LoamTile>(), ModContent.TileType<LoamTileGrass>(), true, Main.tile[i, j].color());          
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
            => num = fail ? 1 : 3;    
    }
    public class LoamTileGrass : ModTile
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
            TileID.Sets.ChecksForMerge[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            TileID.Sets.NeedsGrassFramingDirt[Type] = ModContent.TileType<LoamTile>();

            AddMapEntry(new Color(90, 160, 40));
        }
        #region RandomUpdate, Dust, KillTile
        public override void NumDust(int i, int j, bool fail, ref int num)
            => num = fail ? 1 : 3;
        
        public override void RandomUpdate(int i, int j)
        {
            if (WorldGen.genRand.Next(3) == 0)
                WorldGen.SpreadGrass(i, j, ModContent.TileType<LoamTile>(), ModContent.TileType<LoamTileGrass>(), true, Main.tile[i, j].color());
            
            if (WorldGen.genRand.Next(100) == 0)
                WorldGen.PlaceTile(i, j - 1, ModContent.TileType<DGFoliageTile>());
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            {
                fail = true;
                Main.tile[i, j].type = (ushort)ModContent.TileType<LoamTile>();
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