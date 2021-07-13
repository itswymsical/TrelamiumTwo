using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Common.Worlds
{
    public class DruidsGarden : ModWorld
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int num = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Stalac"));
            if (num != -1)
            {
                tasks.Insert(num + 1, new PassLegacy("World Tree Generation", delegate (GenerationProgress progress)
                {
                    progress.Message = "World Tree & Druid's Garden";
                    int posX = (WorldGen.dungeonX > Main.spawnTileX) ? (Main.spawnTileX - 200) : (Main.spawnTileX  - 200);
                    int baseRaycast = Raycast(posX, (int)Main.worldSurface - 200);

                    if (baseRaycast > (int)Main.worldSurface - 150)
                    {
                        baseRaycast = (int)Main.worldSurface - 150;
                    }
                    if (Main.tile[posX, Raycast(posX, baseRaycast)].type == 147)
                    {
                        posX += ((posX < Main.spawnTileX) ? (-460) : 460);
                    }
                    if (Main.tile[posX, Raycast(posX, baseRaycast)].type == 41 || (Main.tile[posX, Raycast(posX, baseRaycast)].type == 43 || (Main.tile[posX, Raycast(posX, baseRaycast)].type == 44)))
                    {
                        posX += ((posX < Main.spawnTileX) ? (-150) : 150);
                    }
                    if (Main.tile[posX, Raycast(posX, baseRaycast)].type == 60)
                    {
                        posX += ((posX < Main.spawnTileX) ? (-200) : 200);
                    }
                    if (Main.tile[posX, Raycast(posX, baseRaycast)].type == 226)
                    {
                        posX += ((posX < Main.spawnTileX) ? (-100) : 100);
                    }
                    if (Main.tile[posX, Raycast(posX, baseRaycast)].type == 404)
                    {
                        posX += ((posX < Main.spawnTileX) ? (-100) : 100);
                    }
                    int maxValue = 0;
                    int num4 = WorldGen.genRand.Next(5, 8);
                    for (int i = 0; i < num4; i++)
                    {
                        int strengthForBase = baseRaycast + WorldGen.genRand.Next(50, 70) * i;
                        for (int j = 0; j < 20; j++)
                        {
                            int posXRand = WorldGen.genRand.Next(-120, 120);
                            int baseSteps = WorldGen.genRand.Next(0, 120);
                            for (int k = -50; k < 50; k++)
                            {
                                if (posXRand + k >= 20 && posXRand + k <= Main.maxTilesX - 20)
                                {
                                    for (int l = -50; l < 50; l++)
                                    {
                                        if (baseSteps + l >= 20 && baseSteps + l <= Main.maxTilesY - 20)
                                        {
                                            ushort type = Main.tile[posXRand + k, baseSteps + l].type;
                                        }
                                    }
                                }
                            }
                            int num8 = WorldGen.genRand.Next(70, 100);
                            if (Main.tile[posX, baseRaycast].type != TileID.BlueDungeonBrick 
                            || Main.tile[posX, baseRaycast].type != TileID.PinkDungeonBrick 
                            || Main.tile[posX, baseRaycast].type != TileID.GreenDungeonBrick)
                            {
                                WorldGen.TileRunner(posX - posXRand, strengthForBase - baseSteps, (double)num8, 10, ModContent.TileType<Content.Tiles.LoamBlockTile>(), false, 9f, 9f, false, true);
                                WorldGen.SpreadGrass(posX - posXRand, strengthForBase - baseSteps, ModContent.TileType<Content.Tiles.LoamBlockTile>(), ModContent.TileType<Content.Tiles.LoamBlockGrassTile>(), true, Main.tile[i, j].color());
                            }
                            if (strengthForBase - baseSteps < Main.maxTilesY - 170)
                            {
                                SmoothWallRunner(new Point(posX - posXRand, strengthForBase - baseSteps), num8 / 3, WallID.Dirt);
                            }
                        }
                        maxValue = baseRaycast + i * 60;
                    }

                    WorldTree(new Vector2((float)posX, (float)(baseRaycast - 240)));
                    CleanUpTree(new Point(posX, baseRaycast - 240));
                    for (int num9 = 0; num9 < 55; num9++)
                    {
                        WorldGen.TileRunner(posX + WorldGen.genRand.Next(-60, 60), WorldGen.genRand.Next(baseRaycast, maxValue), (double)WorldGen.genRand.Next(8, 14), 10, ModContent.TileType<Content.Tiles.LimestoneTile>(), true, 0f, 0f, false, true);
                    }
                    for (int num9 = 0; num9 < 12; num9++)
                    {
                        WorldGen.Caverer(posX + WorldGen.genRand.Next(-60, 60), WorldGen.genRand.Next(baseRaycast, maxValue));
                    }
                    for (int i = 0; i < Main.maxTilesX; ++i)
                    {
                        for (int k = 0; k < Main.maxTilesY; ++k)
                        {
                            if (Main.tile[i, k].active() && Main.tile[i, k].type == ModContent.TileType<Content.Tiles.LoamBlockTile>() && !Main.tile[i, k - 1].active() && !Main.tile[i, k - 2].active() && WorldGen.genRand.Next(5) == 0)
                            {
                                if (WorldGen.genRand.Next(18) == 0)
                                {
                                    WorldGen.PlaceTile(i, k - 1, ModContent.TileType<Content.Tiles.AlderwoodTree>());
                                }
                            }
                        }
                    }
                }));
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
                WorldGen.TileRunner(leafMiddle.X, leafMiddle.Y + (i * 10), trunkSize * 3, 10, TileID.LivingMahogany, true, 0, 0, false, true);
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