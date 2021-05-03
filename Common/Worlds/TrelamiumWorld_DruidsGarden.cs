using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using Microsoft.Xna.Framework;
using System.Linq;

namespace TrelamiumTwo.Common.Worlds
{
    public sealed partial class TrelamiumWorld : ModWorld
    {
        private void ModifyWorldGenTasks_DruidsGarden(List<GenPass> tasks, ref float totalWeight)
        {
            int num = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Stalac"));
            if (num != -1)
            {
                tasks.Insert(num + 1, new PassLegacy("TrelamiumTwo: Druid's Garden Pass", delegate (GenerationProgress progress)
                {
                    progress.Message = "Druid's Garden";
                    int num2 = (WorldGen.dungeonX > Main.spawnTileX) ? (Main.spawnTileX - 200) : (Main.spawnTileX + 200);
                    int num3 = Raycast(num2, 200);
                    if (Main.maxTilesY == 1800)
                    {
                        num2 = (WorldGen.dungeonX > Main.spawnTileX) ? (Main.spawnTileX - 300) : (Main.spawnTileX + 300);
                    }
                    if (Main.maxTilesY == 2400)
                    {
                        num2 = (WorldGen.dungeonX > Main.spawnTileX) ? (Main.spawnTileX - 400) : (Main.spawnTileX  + 400);
                    }

                if (num3 > (int)Main.worldSurface - 100)
                        num3 = (int)Main.worldSurface - 100;
                    
                    if (num3 < (int)Main.worldSurface - 50)
                        num3 = Raycast(num2, (int)Main.worldSurface - 100) + 160;

                    if (Main.tile[num2, Raycast(num2, num3)].type == 147)
                        num2 += ((num2 < Main.spawnTileX) ? (-460) : 460);
                    
                    if (Main.tile[num2, Raycast(num2, num3)].type == 41 
                    || (Main.tile[num2, Raycast(num2, num3)].type == 43 || (Main.tile[num2, Raycast(num2, num3)].type == 44)))
                        num2 += ((num2 < Main.spawnTileX) ? (-150) : 150);
                    
                    if (Main.tile[num2, Raycast(num2, num3)].type == 60)
                        num2 += ((num2 < Main.spawnTileX) ? (-200) : 200);
                    
                    if (Main.tile[num2, Raycast(num2, num3)].type == 226)
                        num2 += ((num2 < Main.spawnTileX) ? (-100) : 100);
                    
                    if (Main.tile[num2, Raycast(num2, num3)].type == 404)
                        num2 += ((num2 < Main.spawnTileX) ? (-100) : 100);
                    
                    int maxValue = 0;
                    int num4 = WorldGen.genRand.Next(5, 8);
                    for (int i = 0; i < num4; i++)
                    {
                        int num5 = num3 + WorldGen.genRand.Next(50, 70) * i;
                        for (int j = 0; j < 20; j++)
                        {
                            int num6 = WorldGen.genRand.Next(-120, 120);
                            int num7 = WorldGen.genRand.Next(0, 120);
                            for (int k = -50; k < 50; k++)
                            {
                                if (num6 + k >= 20 && num6 + k <= Main.maxTilesX - 20)
                                {
                                    for (int l = -50; l < 50; l++)
                                    {
                                        if (num7 + l >= 20 && num7 + l <= Main.maxTilesY - 20)
                                        {
                                            ushort type = Main.tile[num6 + k, num7 + l].type;
                                        }
                                    }
                                }
                            }
                            int num8 = WorldGen.genRand.Next(70, 100);
                            WorldGen.TileRunner(num2 - num6, num5 - num7, num8, 10,
                                ModContent.TileType<Content.Tiles.DruidsGarden.LoamBlockTile>(), false, 9f, 9f, false, true);
                            if (num5 - num7 < Main.maxTilesY - 200)
                                SmoothWallRunner(new Point(num2 - num6, num5 - num7), num8, 63);

                            int X = num2 - num6;
                            int Y = num5 - num7;
                            int radius = 15;
                            for (int x = X - radius; x <= X + radius; x++)
                            {
                                for (int y = Y - radius; y <= Y + radius; y++)
                                {
                                    if (Vector2.Distance(new Vector2(X, Y), new Vector2(x, y)) <= radius)
                                    {
                                        WorldGen.KillTile(X, Y, false, false, true);
                                    }
                                }
                            }                    
                        }
                        maxValue = num3 + i * 60;
                    }
                    for (int i = 0; i < Main.maxTilesX; ++i)
                    {
                        for (int k = 0; k < Main.maxTilesY; ++k)
                        {
                            if (Main.tile[i, k].active() && Main.tile[i, k].type == ModContent.TileType<Content.Tiles.DruidsGarden.LoamBlockTile>()
                            && !Main.tile[i, k - 1].active() && !Main.tile[i, k - 2].active() && WorldGen.genRand.Next(10) == 0)
                            {
                                WorldGen.PlaceTile(i, k + 1, ModContent.TileType<Content.Tiles.DruidsGarden.AlluviumClusterAlt>()); //TODO sig: Implement Vines
                            }
                        }
                    }
                    WorldTree(new Vector2((float)num2, (float)(num3 - 240)));
                    CleanUpTree(new Point(num2, num3 - 240));
                    for (int m = 0; m < 50; m++)
                    {
                        WorldGen.TileRunner(num2 + WorldGen.genRand.Next(-120, 120), WorldGen.genRand.Next(num3, maxValue), WorldGen.genRand.Next(6, 9), 10, 
                            ModContent.TileType<Content.Tiles.DruidsGarden.LoamBlockTile>(), false, 0f, 0f, false, true);
                    }
                    for (int n = 0; n < 80; n++)
                    {
                        WorldGen.TileRunner(num2 + WorldGen.genRand.Next(-60, 60), WorldGen.genRand.Next(num3, maxValue), WorldGen.genRand.Next(5, 8), 10,
                            ModContent.TileType<Content.Tiles.DruidsGarden.AlluviumOreTile>(), false, 0f, 0f, false, true);
                    }
                    for (int num9 = 0; num9 < 40; num9++)
                    {
                        WorldGen.TileRunner(num2 + WorldGen.genRand.Next(-60, 60), WorldGen.genRand.Next(num3, maxValue), WorldGen.genRand.Next(3, 6), 10,
                            ModContent.TileType<Content.Tiles.DruidsGarden.SlateTile>(), false, 0f, 0f, false, true);
                    }
                    for (int num10 = 0; num10 < 100; num10++)
                    {
                        WorldGen.KillTile(num2 + WorldGen.genRand.Next(-30, 30), WorldGen.genRand.Next(num3, maxValue), false, false, true);
                    }
                    for (int i = 0; i < Main.maxTilesX; ++i)
                    {
                        for (int k = 0; k < Main.maxTilesY; ++k)
                        {
                            if (Main.tile[i, k].active() && Main.tile[i, k].type == ModContent.TileType<Content.Tiles.DruidsGarden.LoamBlockTile>() 
                            && !Main.tile[i, k - 1].active() && !Main.tile[i, k - 2].active() && WorldGen.genRand.Next(10) == 0)
                            {
                                if (WorldGen.genRand.Next(2) == 0)
                                    WorldGen.PlaceTile(i, k - 1, ModContent.TileType<Content.Tiles.DruidsGarden.AlluviumCluster>());
                                
                                else
                                    WorldGen.PlaceTile(i, k - 1, ModContent.TileType<Content.Tiles.DruidsGarden.AlluviumClusterAlt>());                               
                            }
                        }
                    }

                    PlaceAltar(new Point(num2, num3));
                    PlaceDungeons(new Point(num2, num3));
                }));
                num = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Guide"));
                if (num != -1)
                {
                    tasks.Insert(num + 1, new PassLegacy("BiomeChest", delegate (GenerationProgress progress)
                    {
                        progress.Message = "Placing chest";
                        PlaceBiomeChest();
                    }));
                }
            }
        }
        public void WorldTree(Vector2 pos)
        {
            Point leafMiddle = pos.ToPoint();
            for (int i = 0; i < 10; ++i)
            {
                int size = WorldGen.genRand.Next(27, 35);
                Point position = (pos - new Vector2(WorldGen.genRand.Next(-30, 30), WorldGen.genRand.Next(-30, 30))).ToPoint();
                WorldGen.TileRunner(position.X, position.Y, size * 3, 8, TileID.LivingMahoganyLeaves, true, 0, 0, false, true);
            }
            int trunkSize = 10;
            int lastY = 0;
            for (int i = 0; i < 20; ++i)
            {
                WorldGen.TileRunner(leafMiddle.X, leafMiddle.Y + (i * 10), trunkSize * 3, 10, TileID.LivingWood, true, 0, 0, false, true);
                SmoothWallRunnerActive(new Point(leafMiddle.X, leafMiddle.Y + (i * 10)), trunkSize, WallID.LivingWood);
                if (i < 7)
                {
                    if (i % 3 == 0)
                        trunkSize++;
                }
                else if (i < 12)
                    trunkSize++;
                else if (i < 15)
                    trunkSize += WorldGen.genRand.Next(1, 3);
                else
                    trunkSize += 3;
                lastY = leafMiddle.Y + (i * 10) + trunkSize;
                if (i >= 15)
                    WorldGen.TileRunner(leafMiddle.X, leafMiddle.Y + (i * 10) + 15, 25, 10, -1);
            }
            int xSize = 7;
            for (int i = leafMiddle.Y; i < lastY + 5; ++i)
            {
                LineTunnel(new Point(leafMiddle.X, i), xSize);
                int rand = WorldGen.genRand.Next(3);
                if (rand == 0)
                    xSize++;
                if (rand == 1)
                    xSize--;
                if (xSize < 6)
                    xSize++;
                if (xSize > 12)
                    xSize--;
            }
        }
        public void CleanUpTree(Point pos)
        {
            for (int i = -130; i < 130; ++i)
            {
                if (pos.X + i < 20) continue;
                if (pos.X + i > Main.maxTilesX - 20) continue;
                for (int k = -200; k < 200; ++k)
                {
                    if (pos.Y + k < 20) continue;
                    if (pos.Y + k > Main.maxTilesY - 20) continue;
                    if (Main.tile[pos.X + i, pos.Y + k].type == TileID.LivingMahoganyLeaves)
                    {
                        if (Main.tile[pos.X + i, pos.Y + k].active())
                        {
                            try
                            {
                                int tilesConnected = 0;
                                if (Main.tile[pos.X + i, pos.Y + k + 1].active()) tilesConnected++;
                                if (Main.tile[pos.X + i, pos.Y + k - 1].active()) tilesConnected++;
                                if (Main.tile[pos.X + i + 1, pos.Y + k].active()) tilesConnected++;
                                if (Main.tile[pos.X + i - 1, pos.Y + k].active()) tilesConnected++;
                                if (tilesConnected < 2)
                                    WorldGen.KillTile(pos.X + i, pos.Y + k);
                                WorldGen.KillWall(pos.X + i, pos.Y + k);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                }
            }
        }
        public bool CheckPlaceChest(int i, int k)
        {
            bool flag = (Main.tile[i, k].type == 41 && Main.tile[i, k].type == 41) || (Main.tile[i, k].type == 44 && Main.tile[i, k].type == 44) || (Main.tile[i, k].type == 43 && Main.tile[i, k].type == 43);
            bool flag2 = !Main.tile[i, k - 1].active() && !Main.tile[i + 1, k - 1].active() && !Main.tile[i, k - 2].active() && !Main.tile[i + 1, k - 2].active();
            bool flag3 = Main.tile[i, k].active() && Main.tile[i + 1, k].active();

            int[] source = new int[9]
            {7, 94, 95, 98, 99, 8, 96, 97, 9 };

            bool flag4 = source.Any((int x) => x == Main.tile[i, k - 1].wall) && source.Any((int x) => x == Main.tile[i + 1, k - 1].wall);
            if (flag && flag2 && flag3 && flag4)
                return true;
            
            return false;
        }
        public void PlaceBiomeChest()
        {
            int num = (WorldGen.dungeonX < Main.spawnTileX) ? 1 : 2;
            int i;
            int k;
            do
            {
                i = WorldGen.genRand.Next(50, Main.maxTilesX / 2 * num - 50);
                k = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 200);
            }
            while (!CheckPlaceChest(i, k));
        }
        private void PlaceAltar(Point centre)
        {
            int num = 1;
            for (int i = 0; i < num; i++)
            {
                Point pos = new Point(centre.X + WorldGen.genRand.Next(-120, 120), centre.Y + WorldGen.genRand.Next(-120, 220));
                MakeAltar(pos, -1, -1, WorldGen.genRand.Next(2) == 0);
            }
        }
        private void PlaceDungeons(Point centre)
        {
            int num = 8;
            if (Main.maxTilesY == 1800)
            {
                num = 12;
            }
            if (Main.maxTilesY == 2400)
            {
                num = 16;
            }
            for (int i = 0; i < num; i++)
            {
                Point pos = new Point(centre.X + WorldGen.genRand.Next(-120, 120), centre.Y + WorldGen.genRand.Next(-120, 220));
                MakeRuin(pos, -1, -1, WorldGen.genRand.Next(2) == 0);
            }
        }
        public Point MakeAltar(Point pos, int type = -1, int wType = -1, bool doRep = true)
        {
            int num = 24;
            int num2 = num - 14;
            int type2 = (type != -1) ? type : TileID.GrayBrick;
            int type3 = (wType != -1) ? wType : 147;
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    WorldGen.KillTile(pos.X + i, pos.Y + j, false, false, false);
                    WorldGen.KillTile(pos.X + i, pos.Y - 1 + j, false, false, false);
                    WorldGen.KillTile(pos.X + i, pos.Y + 1 + j, false, false, false);
                    WorldGen.KillWall(pos.X + i, pos.Y + j, false);
                    WorldGen.PlaceWall(pos.X + i, pos.Y + j, type3, false);
                }
            }
            for (int k = 0; k < num; k++)
            {
                KillPlaceTile(pos.X + k, pos.Y, type2, 0);
                KillPlaceTile(pos.X + k, pos.Y - 1, type2, 0);
                KillPlaceTile(pos.X + k, pos.Y + num2, type2, 0);
                KillPlaceTile(pos.X + k, pos.Y + 1 + num2, type2, 0);
            }
            for (int l = 0; l < num2; l++)
            {
                KillPlaceTile(pos.X, pos.Y + l, type2, 0);
                KillPlaceTile(pos.X + num, pos.Y + l, type2, 0);
            }
            KillPlaceTile(pos.X + num, pos.Y + num2, type2, 0);
            KillPlaceTile(pos.X + num, pos.Y + num2 + 1, type2, 0);
            KillPlaceTile(pos.X + num, pos.Y - 1, type2, 0);
            int num3 = pos.Y + (num2 - 3);
            int num4 = pos.X + num / 2;

            WorldGen.PlaceTile(num4, num3 + 2, type2, true, false, -1, 23);
            WorldGen.PlaceTile(num4 + 1, num3 + 2, type2, true, false, -1, 23);
            WorldGen.PlaceTile(num4 - 1, num3 + 2, type2, true, false, -1, 23);
            WorldGen.PlaceTile(num4 + 2, num3 + 2, type2, true, false, -1, 23);
            WorldGen.PlaceTile(num4 - 2, num3 + 2, type2, true, false, -1, 23);
            WorldGen.PlaceTile(num4, num3 + 2, type2, true, false, -1, 23);

            WorldGen.PlaceTile(num4, num3 + 1, type2, true, false, -1, 23);
            WorldGen.PlaceTile(num4 + 1, num3 + 1, type2, true, false, -1, 23);
            WorldGen.PlaceTile(num4 - 1, num3 + 1, type2, true, false, -1, 23);

            WorldGen.PlaceChest(num4 - 5, num3, 
                (ushort)ModContent.TileType<Content.Tiles.DruidsGarden.AncientTotem>(), false, 0);
            WorldGen.PlaceChest(num4 + 6, num3, 
                (ushort)ModContent.TileType<Content.Tiles.DruidsGarden.AncientTotem>(), false, 0);
            WorldGen.PlaceChest(num4, num3,
                (ushort)ModContent.TileType<Content.Tiles.DruidsGarden.AncientAltar>(), false, 0);
            return new Point(pos.X + num, pos.Y + num2);
        }
        public Point MakeRuin(Point pos, int type = -1, int wType = -1, bool doRep = true)
        {
            int num = WorldGen.genRand.Next(12, 16);
            int num2 = num - 5;
            int type2 = (type != -1) ? type : TileID.GrayBrick;
            int type3 = (wType != -1) ? wType : 147;
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    WorldGen.KillTile(pos.X + i, pos.Y + j, false, false, false);
                    WorldGen.KillTile(pos.X + i, pos.Y - 1 + j, false, false, false);
                    WorldGen.KillTile(pos.X + i, pos.Y + 1 + j, false, false, false);
                    WorldGen.KillWall(pos.X + i, pos.Y + j, false);
                    WorldGen.PlaceWall(pos.X + i, pos.Y + j, type3, false);
                }
            }
            for (int k = 0; k < num; k++)
            {
                KillPlaceTile(pos.X + k, pos.Y, type2, 0);
                KillPlaceTile(pos.X + k, pos.Y - 1, type2, 0);
                KillPlaceTile(pos.X + k, pos.Y + num2, type2, 0);
                KillPlaceTile(pos.X + k, pos.Y + 1 + num2, type2, 0);
            }
            for (int l = 0; l < num2; l++)
            {
                KillPlaceTile(pos.X, pos.Y + l, type2, 0);
                KillPlaceTile(pos.X + num, pos.Y + l, type2, 0);
            }
            KillPlaceTile(pos.X + num, pos.Y + num2, type2, 0);
            KillPlaceTile(pos.X + num, pos.Y + num2 + 1, type2, 0);
            KillPlaceTile(pos.X + num, pos.Y - 1, type2, 0);
            int num3 = pos.Y + (num2 - 1);
            num3 -= ((WorldGen.genRand.Next(2) == 0) ? 3 : 0);
            int num4 = pos.X + WorldGen.genRand.Next(3, num - 3);
            if (num3 != pos.Y + (num2 - 1))
            {
                WorldGen.PlaceTile(num4, num3 + 1, 19, true, false, -1, 23);
                WorldGen.PlaceTile(num4 + 1, num3 + 1, 19, true, false, -1, 23);
            }
            WorldGen.PlaceChest(num4, num3, TileID.Containers, false, 10);
            return new Point(pos.X + num, pos.Y + num2);
        }
        #region Runners
        public void SmoothWallRunner(Point position, int size, int wallID)
        {
            for (int i = position.X - size; i <= position.X + size; i++)
            {
                for (int j = position.Y - size; j <= position.Y + size; j++)
                {
                    bool flag = i > 10 && i < Main.maxTilesX - 10 && j > 10 && j < Main.maxTilesY - 10;
                    if (Vector2.Distance(new Vector2((float)position.X, (float)position.Y), new Vector2((float)i, (float)j)) <= (float)size && flag && Main.tile[i, j] != null)
                    {
                        WorldGen.KillWall(i, j, false);
                        WorldGen.PlaceWall(i, j, (ushort)wallID, true);
                    }
                }
            }
        }
        public void SmoothWallRunnerActive(Point position, int size, int wallID)
        {
            for (int i = position.X - size; i <= position.X + size; i++)
            {
                for (int j = position.Y - size; j <= position.Y + size; j++)
                {
                    bool flag = i > 10 && i < Main.maxTilesX - 10 && j > 10 && j < Main.maxTilesY - 10;
                    if (Vector2.Distance(new Vector2((float)position.X, (float)position.Y), new Vector2((float)i, (float)j)) <= (float)size && flag && Main.tile[i, j] != null && Main.tile[i, j].active())
                    {
                        WorldGen.KillWall(i, j, false);
                        WorldGen.PlaceWall(i, j, (ushort)wallID, true);
                    }
                }
            }
        }
        public void SmoothTileRunner(Point position, int size, int type)
        {
            for (int i = position.X - size; i <= position.X + size; i++)
            {
                for (int j = position.Y - size; j <= position.Y + size; j++)
                {
                    if (Vector2.Distance(new Vector2((float)position.X, (float)position.Y), new Vector2((float)i, (float)j)) <= (float)size && Main.tile[i, j] != null)
                    {
                        WorldGen.KillTile(i, j, false, false, true);
                        WorldGen.PlaceTile(i, j, (ushort)type, true, true, -1, 0);
                    }
                }
            }
        }
        public void SmoothTunnel(Point position, int size)
        {
            for (int i = position.X - size; i <= position.X + size; i++)
            {
                for (int j = position.Y - size; j <= position.Y + size; j++)
                {
                    bool flag = i > 10 && i < Main.maxTilesX - 10 && j > 10 && j < Main.maxTilesY - 10;
                    if (Vector2.Distance(new Vector2((float)position.X, (float)position.Y), new Vector2((float)i, (float)j)) <= (float)size && flag && Main.tile[i, j] != null)
                    {
                        WorldGen.KillTile(i, j, false, false, true);
                    }
                }
            }
        }
        public void SquareRunner(Point position, int size, int type)
        {
            for (int i = position.X - size; i <= position.X + size; i++)
            {
                for (int j = position.Y - size; j <= position.Y + size; j++)
                {
                    KillPlaceTile(i, j, (ushort)type, 0);
                }
            }
        }
        public void LineTunnel(Point position, int xReps)
        {
            for (int i = position.X - xReps / 2; i <= position.X + xReps / 2; i++)
            {
                WorldGen.KillTile(i, position.Y, false, false, true);
            }
        }
        public void KillPlaceTile(int x, int y, int type, int style = 0)
        {
            WorldGen.KillTile(x, y, false, false, true);
            WorldGen.PlaceTile(x, y, type, true, true, -1, style);
        }
        #endregion
        public int Raycast(int x, int y)
        {
            if (x < 2 || x > Main.maxTilesX - 2)
            {
                mod.Logger.Error("X is dead.");
                return 0;
            }
            if (y < 2 || y > Main.maxTilesY - 2)
            {
                mod.Logger.Error("Y is not alive");
                return 0;
            }
            while (!Main.tile[x, y].active())
            {
                y++;
            }
            return y;
        }
    }
}