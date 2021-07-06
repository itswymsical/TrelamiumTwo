using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;

using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Common.Worlds
{
    public partial class TrelamiumWorld : ModWorld
    {
        private void ModifyWorldGenTasks_TestBiome(List<GenPass> tasks)
        {
            int num = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Grass Wall"));
            if (num != -1)
            {
                int placementX = Main.spawnTileX;
                int placementY = Raycast(placementX, (int)Main.worldSurface - 100);
                int strength = WorldGen.genRand.Next(70, 100);

                tasks.Insert(num + 1, new PassLegacy("TrelamiumTwo: Test Biome", delegate (GenerationProgress progress)
                {
                    int maxValue = 0;
                    for (int i = 0; i < 6; i++)
                    {
                        int newPosY = placementY + WorldGen.genRand.Next(50, 70) * i;
                        for (int j = 0; j < 20; j++)
                        {
                            int offsetX = WorldGen.genRand.Next(-120, 120);
                            int offsetY = WorldGen.genRand.Next(0, 120);
                            for (int k = -50; k < 50; k++)
                                if (offsetX + k >= 20 && offsetX + k <= Main.maxTilesX - 20)
                                    for (int l = -50; l < 50; l++)
                                        if (offsetY + l >= 20 && offsetY + l <= Main.maxTilesY - 20) {
                                            ushort type = Main.tile[offsetX + k, offsetY + l].type;
                                        }

                            WorldGen.TileRunner(placementX - offsetX, offsetY - offsetY, strength, 10, TileID.Pearlstone, false, 9f, 9f, false, true);
                            WorldGen.digTunnel(placementX - (offsetX * Main.rand.Next(60)), placementY - (offsetY * Main.rand.Next(60)), 1, 3, 30, 25); // Cave method

                            int X = placementX - offsetX;
                            int Y = newPosY - offsetY;
                            int radius = 8;

                            for (int x = X - radius; x <= X + radius; x++)
                            {
                                for (int y = Y - radius; y <= Y + radius; y++)
                                {
                                    if (Vector2.Distance(new Vector2(X, Y), new Vector2(x, y)) <= radius)
                                    {
                                        WorldGen.KillTile(placementX - offsetX, newPosY - offsetY, false, false, true);
                                    }
                                }
                            }
                        }
                        maxValue = placementY + i * 60;
                    }
                }));
            }
        }
    }
}