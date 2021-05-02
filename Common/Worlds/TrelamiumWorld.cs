#region Using directives
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

using TrelamiumTwo.Content.Tiles.DustifiedCaverns;
using TrelamiumTwo.Content.Tiles.DruidsGarden;
#endregion

namespace TrelamiumTwo.Common.Worlds
{
    public sealed partial class TrelamiumWorld : ModWorld
	{
		public static bool downedFungore;
		public static bool downedGlacier;
		public static int DruidsGardenTiles;
		public static int DustifiedCavernTiles;
		public override void Initialize()
        {
			downedFungore = false;
			downedGlacier = false;
		}
		public override void TileCountsAvailable(int[] tileCounts)
        {
            DustifiedCavernTiles = 
				tileCounts[ModContent.TileType<DustiliteCrystal_Large1>()];

            DruidsGardenTiles = 
				tileCounts[ModContent.TileType<LoamBlockTile>()] + 
				tileCounts[ModContent.TileType<LoamBlockGrassTile>()];
        }
		#region TagCompound & Loading
		public override TagCompound Save()
		{
			var downed = new List<string>();
			if (downedFungore)
			{
				downed.Add("Fungore");
			}
			if (downedFungore)
			{
				downed.Add("Glacier");
			}
			return new TagCompound
			{
			};
		}
		public override void Load(TagCompound tag)
		{
			var downed = tag.GetList<string>("downed");
			downedFungore = downed.Contains("Fungore");
			downedGlacier = downed.Contains("Glacier");
		}

		public override void LoadLegacy(BinaryReader reader)
		{
			int loadVersion = reader.ReadInt32();
			if (loadVersion == 0)
			{
				BitsByte flags = reader.ReadByte();
				downedFungore = flags[0];
				downedGlacier = flags[1];
			}
			else
			{
				mod.Logger.WarnFormat("TrelamiumTwo: Unknown loadVersion: {0}", loadVersion);
			}
		}

		public override void NetSend(BinaryWriter writer)
		{
			var flags = new BitsByte();
			flags[0] = downedFungore;
			flags[1] = downedGlacier;
			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			downedFungore = flags[0];
			downedGlacier = flags[1];
		}
#endregion

		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			//ModifyWorldGenTasks_Campsites(tasks, ref totalWeight);
			ModifyWorldGenTasks_DustifiedCaverns(tasks, ref totalWeight);
			ModifyWorldGenTasks_DruidsGarden(tasks, ref totalWeight);
		}

		public override void ResetNearbyTileEffects()
		{
			DustifiedCavernTiles = 0;
			DruidsGardenTiles = 0;
		}

		public override void PostWorldGen()
		{
			//PostWorldGen_SpawnChestContent();
		}
	}
}