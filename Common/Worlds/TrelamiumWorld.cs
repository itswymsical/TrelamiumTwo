#region Using directives

using System.Collections.Generic;

using Terraria.ModLoader;
using Terraria.World.Generation;

using TrelamiumTwo.Content.Tiles.DustifiedCaverns;

#endregion


namespace TrelamiumTwo.Common.Worlds
{
    public sealed partial class TrelamiumWorld : ModWorld
    {
        public override void Initialize()
        {
        }
		public static int DustifiedCavernTiles;

		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			//ModifyWorldGenTasks_Campsites(tasks, ref totalWeight);
			ModifyWorldGenTasks_DustifiedCaverns(tasks, ref totalWeight);
			ModifyWorldGenTasks_DruidsGarden(tasks, ref totalWeight);
		}

		public override void ResetNearbyTileEffects()
		{
			DustifiedCavernTiles = 0;
		}

		public override void TileCountsAvailable(int[] tileCounts)
		{
			/*DustifiedCavernTiles =
				tileCounts[ModContent.TileType<Ironsand>()] +
				tileCounts[ModContent.TileType<Huskstone>()] +
				tileCounts[ModContent.TileType<AetherousSoil>()];*/
		}

		public override void PostWorldGen()
		{
			//PostWorldGen_SpawnChestContent();
		}
	}
}