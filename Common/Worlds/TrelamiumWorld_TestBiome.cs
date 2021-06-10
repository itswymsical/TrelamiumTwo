using System.Collections.Generic;
using Terraria;

using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;

namespace TrelamiumTwo.Common.Worlds
{
	public partial class TrelamiumWorld : ModWorld
	{
		private void ModifyWorldGenTasks_TestBiome(List<GenPass> tasks)
		{
			FastNoiseLite seededPerlin = new FastNoiseLite(Main.ActiveWorldFileData.Seed);
			int num = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Stalac"));
			if (num != -1)
			{
				tasks.Insert(num + 1, new PassLegacy("Initialization", delegate (GenerationProgress progress)
				{
					progress.Message = "Generating Large Dunes";
					seededPerlin.SetNoiseType(FastNoiseLite.NoiseType.ValueCubic);
					seededPerlin.SetSeed(1337);
					seededPerlin.SetFrequency(0.022f);
					seededPerlin.SetFractalType(FastNoiseLite.FractalType.Ridged);
					seededPerlin.SetFractalOctaves(2);
					seededPerlin.SetDomainWarpType(FastNoiseLite.DomainWarpType.OpenSimplex2);
					seededPerlin.SetDomainWarpAmp(30f);
				}));
				tasks.Insert(num + 1, new PassLegacy("Surface Generation", delegate (GenerationProgress progress)
				{
					progress.Message = "Generating Perlin Caves";
					for (int num851 = 0; num851 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.00013); num851++)
					{
						float value17 = (float)((double)num851 / ((double)(Main.maxTilesX * Main.maxTilesY) * 0.00013));
						progress.Set(value17);
						if (WorldGen.rockLayerHigh <= (double)Main.maxTilesY)
						{
							WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)WorldGen.rockLayerHigh, Main.maxTilesY), WorldGen.genRand.Next(6, 20), WorldGen.genRand.Next(50, 70), -1);
						}
					}
				}));
			}
		}
	}
}