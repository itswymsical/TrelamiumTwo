#region Using directives

using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

using TrelamiumTwo.Content.Tiles.DustifiedCaverns;

#endregion


namespace TrelamiumTwo.Common.Worlds
{
    public sealed partial class TrelamiumWorld : ModWorld
	{
		public static bool downedFungore;
		public override void Initialize()
        {
			downedFungore = false;
		}
        #region TagCompound & Loading
        public override TagCompound Save()
		{
			var downed = new List<string>();
			if (downedFungore)
			{
				downed.Add("Fungore");
			}
			return new TagCompound
			{
			};
		}
		public override void Load(TagCompound tag)
		{
			var downed = tag.GetList<string>("downed");
			downedFungore = downed.Contains("Fungore");
		}

		public override void LoadLegacy(BinaryReader reader)
		{
			int loadVersion = reader.ReadInt32();
			if (loadVersion == 0)
			{
				BitsByte flags = reader.ReadByte();
				downedFungore = flags[0];
			}
			else
			{
				mod.Logger.WarnFormat("PrimordialSands: Unknown loadVersion: {0}", loadVersion);
			}
		}

		public override void NetSend(BinaryWriter writer)
		{
			var flags = new BitsByte();
			flags[0] = downedFungore;
			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			downedFungore = flags[0];
		}
#endregion

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