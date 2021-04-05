using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using static Terraria.ModLoader.ModContent;

namespace PrimordialSands.Common.Worlds
{
    public class EngulfedIsle : ModWorld
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int num = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Stalac"));
            if (num != -1)
            {
                tasks.Insert(num + 1, new PassLegacy("Tree of Life Generation", delegate (GenerationProgress progress)
                {
                    progress.Message = "Growning Eden...";
                    int basePosition = (WorldGen.dungeonX > Main.spawnTileX) ? (Main.spawnTileX + Main.rand.Next(120, 400)) : (Main.spawnTileX - Main.rand.Next(120, 400));
                    if (Main.dungeonX > Main.spawnTileX)
                    {
                        basePosition = (WorldGen.dungeonX > Main.spawnTileX) ? (Main.spawnTileX + Main.rand.Next(120, 400)) : (Main.spawnTileX - Main.rand.Next(120, 400));
                    }
                    int baseRaycast = Raycast(basePosition, 200);

                    if (Main.maxTilesY == 1800)
                    {
                        basePosition = (WorldGen.dungeonX > Main.spawnTileX) ? (Main.spawnTileX + Main.rand.Next(220, 500)) : (Main.spawnTileX - Main.rand.Next(220, 500));
                    }
                    if (Main.maxTilesY == 2400)
                    {
                        basePosition = (WorldGen.dungeonX > Main.spawnTileX) ? (Main.spawnTileX + Main.rand.Next(420, 600)) : (Main.spawnTileX - Main.rand.Next(420, 600));
                    }
                    if (baseRaycast > (int)Main.worldSurface - 200)
                    {
                        baseRaycast = (int)Main.worldSurface - 200;
                    }
                    if (baseRaycast < (int)Main.worldSurface - 150)
                    {
                        baseRaycast = Raycast(basePosition, (int)Main.worldSurface - 50) + 134;
                    }
                    if (Main.tile[basePosition, Raycast(basePosition, baseRaycast)].type == 147)
                    {
                        basePosition += ((basePosition < Main.spawnTileX) ? (-460) : 460);
                    }
                    if (Main.tile[basePosition, Raycast(basePosition, baseRaycast)].type == 41 || (Main.tile[basePosition, Raycast(basePosition, baseRaycast)].type == 43 || (Main.tile[basePosition, Raycast(basePosition, baseRaycast)].type == 44)))
                    {
                        basePosition += ((basePosition < Main.spawnTileX) ? (-150) : 150);
                    }
                    if (Main.tile[basePosition, Raycast(basePosition, baseRaycast)].type == 60)
                    {
                        basePosition += ((basePosition < Main.spawnTileX) ? (-200) : 200);
                    }
                    if (Main.tile[basePosition, Raycast(basePosition, baseRaycast)].type == 226)
                    {
                        basePosition += ((basePosition < Main.spawnTileX) ? (-100) : 100);
                    }
                    if (Main.tile[basePosition, Raycast(basePosition, baseRaycast)].type == 404)
                    {
                        basePosition += ((basePosition < Main.spawnTileX) ? (-100) : 100);
                    }
                    int maxValue = 0;
                    int num4 = WorldGen.genRand.Next(5, 8);
                    for (int i = 0; i < num4; i++)
                    {
                        int strengthForBase = baseRaycast + WorldGen.genRand.Next(50, 70) * i;
                        for (int j = 0; j < 20; j++)
                        {
                            int basePositionRand = WorldGen.genRand.Next(-120, 120);
                            int baseSteps = WorldGen.genRand.Next(0, 120);
                            for (int k = -50; k < 50; k++)
                            {
                                if (basePositionRand + k >= 20 && basePositionRand + k <= Main.maxTilesX - 20)
                                {
                                    for (int l = -50; l < 50; l++)
                                    {
                                        if (baseSteps + l >= 20 && baseSteps + l <= Main.maxTilesY - 20)
                                        {
                                            ushort type = Main.tile[basePositionRand + k, baseSteps + l].type;
                                        }
                                    }
                                }
                            }
                            int num8 = WorldGen.genRand.Next(70, 100);
                            if (Main.tile[basePosition, baseRaycast].type != TileID.BlueDungeonBrick || Main.tile[basePosition, baseRaycast].type != TileID.PinkDungeonBrick || Main.tile[basePosition, baseRaycast].type != TileID.GreenDungeonBrick)
                            { //This will skip all of these blocks.
                                WorldGen.TileRunner(basePosition - basePositionRand, strengthForBase - baseSteps, (double)num8, 10, TileType<Blessed_Dirt>(), true, 9f, 9f, false, true);
                                WorldGen.SpreadGrass(basePosition - basePositionRand, strengthForBase - baseSteps, TileType<Blessed_Dirt>(), TileType<Floragrass>(), true, Main.tile[i, j].color());
                            }
                            if (strengthForBase - baseSteps < Main.maxTilesY - 170)
                            {
                                SmoothWallRunner(new Point(basePosition - basePositionRand, strengthForBase - baseSteps), num8 / 3, WallType<Blessed_Dirt_Wall>());
                            }
                        }
                        maxValue = baseRaycast + i * 60;
                    }

                    MakeTropicalTree(new Vector2((float)basePosition, (float)(baseRaycast - 240)));
                    CleanUpTree(new Point(basePosition, baseRaycast - 240));
                    for (int num9 = 0; num9 < 55; num9++)
                    {
                        WorldGen.TileRunner(basePosition + WorldGen.genRand.Next(-60, 60), WorldGen.genRand.Next(baseRaycast, maxValue), (double)WorldGen.genRand.Next(8, 14), 10, TileType<Slate_Tile>(), true, 0f, 0f, false, true);
                    }
                    for (int num11 = 0; num11 < 30; num11++)
                    {
                        WorldGen.CaveOpenater(basePosition + WorldGen.genRand.Next(-40, 40), WorldGen.genRand.Next(baseRaycast, maxValue));
                    }
                    for (int num12 = 0; num12 < 60; num12++)
                    {
                        WorldGen.Caverer(basePosition + WorldGen.genRand.Next(-40, 40), WorldGen.genRand.Next(baseRaycast, maxValue));
                    }

                    for (int i = 0; i < Main.maxTilesX; ++i)
                    {
                        for (int k = 0; k < Main.maxTilesY; ++k)
                        {
                            if (Main.tile[i, k].active() && Main.tile[i, k].type == TileType<Blessed_Dirt>() && !Main.tile[i, k - 1].active() && !Main.tile[i, k - 2].active() && WorldGen.genRand.Next(5) == 0)
                            {
                                if (WorldGen.genRand.Next(2) == 0)
                                {
                                    WorldGen.PlaceTile(i, k - 1, TileType<Indenwood_Sapling_Tile>());
                                }
                                else
                                {
                                    WorldGen.PlaceTile(i, k - 1, TileType<Indenwood_Sapling_Tile>());
                                }
                            }
                        }
                    }

                    PlaceAltar(new Point(basePosition, baseRaycast));
                    PlaceDungeons(new Point(basePosition, baseRaycast));
                }));
                num = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Guide"));
                if (num != -1)
                {
                    tasks.Insert(num + 1, new PassLegacy("BiomeChest", delegate (GenerationProgress progress)
                    {
                        progress.Message = "Tree of Life Chests";
                        PlaceBiomeChest();
                    }));
                }
            }
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
        public bool CheckPlaceChest(int i, int k)
        {
            bool flag = (Main.tile[i, k].type == 41 && Main.tile[i, k].type == 41) || (Main.tile[i, k].type == 44 && Main.tile[i, k].type == 44) || (Main.tile[i, k].type == 43 && Main.tile[i, k].type == 43);
            bool flag2 = !Main.tile[i, k - 1].active() && !Main.tile[i + 1, k - 1].active() && !Main.tile[i, k - 2].active() && !Main.tile[i + 1, k - 2].active();
            bool flag3 = Main.tile[i, k].active() && Main.tile[i + 1, k].active();
            int[] source = new int[9]
            {
            7,
            94,
            95,
            98,
            99,
            8,
            96,
            97,
            9
            };
            bool flag4 = source.Any((int x) => x == Main.tile[i, k - 1].wall) && source.Any((int x) => x == Main.tile[i + 1, k - 1].wall);
            if (flag && flag2 && flag3 && flag4)
            {
                return true;
            }
            return false;
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
            int basePosition = num - 14;
            int type2 = (type != -1) ? type : TileType<Slate_Tile>();
            int type3 = (wType != -1) ? wType : 147;
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < basePosition; j++)
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
                KillPlaceTile(pos.X + k, pos.Y + basePosition, type2, 0);
                KillPlaceTile(pos.X + k, pos.Y + 1 + basePosition, type2, 0);
            }
            for (int l = 0; l < basePosition; l++)
            {
                KillPlaceTile(pos.X, pos.Y + l, type2, 0);
                KillPlaceTile(pos.X + num, pos.Y + l, type2, 0);
            }
            KillPlaceTile(pos.X + num, pos.Y + basePosition, type2, 0);
            KillPlaceTile(pos.X + num, pos.Y + basePosition + 1, type2, 0);
            KillPlaceTile(pos.X + num, pos.Y - 1, type2, 0);
            int baseRaycast = pos.Y + (basePosition - 3);
            int num4 = pos.X + num / 2;

            WorldGen.PlaceTile(num4, baseRaycast + 2, type2, true, false, -1, 23);
            WorldGen.PlaceTile(num4 + 1, baseRaycast + 2, type2, true, false, -1, 23);
            WorldGen.PlaceTile(num4 - 1, baseRaycast + 2, type2, true, false, -1, 23);
            WorldGen.PlaceTile(num4 + 2, baseRaycast + 2, type2, true, false, -1, 23);
            WorldGen.PlaceTile(num4 - 2, baseRaycast + 2, type2, true, false, -1, 23);
            WorldGen.PlaceTile(num4, baseRaycast + 2, type2, true, false, -1, 23);

            WorldGen.PlaceTile(num4, baseRaycast + 1, type2, true, false, -1, 23);
            WorldGen.PlaceTile(num4 + 1, baseRaycast + 1, type2, true, false, -1, 23);
            WorldGen.PlaceTile(num4 - 1, baseRaycast + 1, type2, true, false, -1, 23);

            WorldGen.PlaceChest(num4 - 5, baseRaycast, (ushort)TileType<Ruin_Chest_Tile>(), false, 0);
            WorldGen.PlaceChest(num4 + 6, baseRaycast, (ushort)TileType<Ruin_Chest_Tile>(), false, 0);
            WorldGen.PlaceChest(num4, baseRaycast, (ushort)TileType<Ruin_Chest_Tile>(), false, 0);
            return new Point(pos.X + num, pos.Y + basePosition);
        }
        public Point MakeRuin(Point pos, int type = -1, int wType = -1, bool doRep = true)
        {
            int num = WorldGen.genRand.Next(12, 16);
            int basePosition = num - 5;
            int type2 = (type != -1) ? type : TileID.ChlorophyteBrick;
            int type3 = (wType != -1) ? wType : 147;
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < basePosition; j++)
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
                KillPlaceTile(pos.X + k, pos.Y + basePosition, type2, 0);
                KillPlaceTile(pos.X + k, pos.Y + 1 + basePosition, type2, 0);
            }
            for (int l = 0; l < basePosition; l++)
            {
                KillPlaceTile(pos.X, pos.Y + l, type2, 0);
                KillPlaceTile(pos.X + num, pos.Y + l, type2, 0);
            }
            KillPlaceTile(pos.X + num, pos.Y + basePosition, type2, 0);
            KillPlaceTile(pos.X + num, pos.Y + basePosition + 1, type2, 0);
            KillPlaceTile(pos.X + num, pos.Y - 1, type2, 0);
            int baseRaycast = pos.Y + (basePosition - 1);
            baseRaycast -= ((WorldGen.genRand.Next(2) == 0) ? 3 : 0);
            int num4 = pos.X + WorldGen.genRand.Next(3, num - 3);
            if (baseRaycast != pos.Y + (basePosition - 1))
            {
                WorldGen.PlaceTile(num4, baseRaycast + 1, 19, true, false, -1, 23);
                WorldGen.PlaceTile(num4 + 1, baseRaycast + 1, 19, true, false, -1, 23);
            }
            WorldGen.PlaceChest(num4, baseRaycast, (ushort)TileType<Ruin_Chest_Tile>(), false, 0);
            return new Point(pos.X + num, pos.Y + basePosition);
        }
        public void KillPlaceTile(int x, int y, int type, int style = 0)
        {
            WorldGen.KillTile(x, y, false, false, true);
            WorldGen.PlaceTile(x, y, type, true, true, -1, style);
        }
        public void MakeTropicalTree(Vector2 pos)
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
            int dirTunnel = WorldGen.genRand.Next(2) == 0 ? -1 : 1;
            for (int i = 0; i < 10; ++i)
            {
                if (Main.tile[leafMiddle.X - 5 + i, leafMiddle.Y + 9].active()) continue;
                KillPlaceTile(leafMiddle.X - 5 + i, leafMiddle.Y + 9, TileID.Platforms, 23);
            }
            int roomSide = WorldGen.genRand.Next(2) == 0 ? -1 : 1;
            int roomX = roomSide == 1 ? 10 : -20;
            int roomY = leafMiddle.Y + WorldGen.genRand.Next(10, 15);
            Point bottomCorner = MakeRuin(new Point(leafMiddle.X + roomX, roomY), TileID.LivingMahogany, WallType<Slate_Wall>(), false);

            for (int i = 0; i < Math.Abs(roomX + 2); ++i)
            {
                for (int k = 0; k < 3; ++k)
                {
                    WorldGen.KillTile(leafMiddle.X + (i * roomSide), bottomCorner.Y - k - 1);
                    if (WorldGen.genRand.Next(3) == 0)
                        WorldGen.PlaceTile(leafMiddle.X + (i * roomSide), bottomCorner.Y - k - 1, TileID.Cobweb);
                }
            }
        }
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
        public int Raycast(int x, int y)
        {
            if (x < 2 || x > Main.maxTilesX - 2)
            {
                ErrorLogger.Log("X is dead.");
                return 0;
            }
            if (y < 2 || y > Main.maxTilesY - 2)
            {
                ErrorLogger.Log("Y is not alive");
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