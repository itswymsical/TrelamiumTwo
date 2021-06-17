using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Common.Players
{
    public partial class TrelamiumPlayer : ModPlayer
    {
        public void ShovelPickTile(int x, int y)
        {
            int digTile = player.HeldItem.GetGlobalItem<Globals.GlobalTrelamiumItem>().digPower;
            for (int i = -1; i < 2; i++)
            {
                int posx = x / 16 + i;
                int posy = y / 16 + i;
                if (Main.tile[posx, y / 16].type != TileID.DemonAltar && Main.tile[x / 16, posy].type != TileID.DemonAltar
                    && Main.tile[posx, y / 16].type != TileID.Trees && Main.tile[x / 16, posy].type != TileID.Trees
                    && Main.tile[posx, y / 16].type != TileID.PalmTree && Main.tile[x / 16, posy].type != TileID.PalmTree
                    && Main.tile[posx, y / 16].type != TileID.MushroomTrees && Main.tile[x / 16, posy].type != TileID.MushroomTrees
                    && Main.tile[posx, y / 16].type != TileID.ShadowOrbs && Main.tile[x / 16, posy].type != TileID.ShadowOrbs
                    && Main.tile[posx, y / 16].type != TileID.Cactus && Main.tile[x / 16, posy].type != TileID.Cactus)
                {
                    player.PickTile(posx, y / 16, digTile);
                    player.PickTile(x / 16, posy, digTile);
                }
            }
        }
    }
}
