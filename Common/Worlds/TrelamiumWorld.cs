using System.Collections.Generic;
using System.IO;

using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace TrelamiumTwo.Common.Worlds
{
	public partial class TrelamiumWorld : ModWorld
	{
		public static bool downedFungore;
		public static bool initialCutscene;
		public override void Initialize()
		{
			downedFungore = false;
			initialCutscene = false;
		}
		
		public override TagCompound Save()
		{
			var downed = new List<string>();
			if (downedFungore)
			{
				downed.Add("Fungore");
			}
			if (initialCutscene)
			{
				downed.Add("Initial_Cutscene");
			}
			return new TagCompound
			{
			};
		}
		public override void Load(TagCompound tag)
		{
			var downed = tag.GetList<string>("downed");
			downedFungore = downed.Contains("Fungore");
			initialCutscene = downed.Contains("Initial_Cutscene");
		}

		public override void LoadLegacy(BinaryReader reader)
		{
			int loadVersion = reader.ReadInt32();
			if (loadVersion == 0)
			{
				BitsByte flags = reader.ReadByte();
				downedFungore = flags[0];
				initialCutscene = flags[1];
			}
			else
			{
				Mod.Logger.WarnFormat("TrelamiumTwo: Unknown loadVersion: {0}", loadVersion);
			}
		}

		public override void NetSend(BinaryWriter writer)
		{
			var flags = new BitsByte();
			flags[0] = downedFungore;
			flags[1] = initialCutscene;
			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			downedFungore = flags[0];
			initialCutscene = flags[1];
		}

		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			ModifyWorldGenTasks_TestBiome(tasks);
			int shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
		}
		public int Raycast(int x, int y)
		{
			if (x < 2 || x > Main.maxTilesX - 2)
			{
				Mod.Logger.Error("X is dead.");
				return 0;
			}
			if (y < 2 || y > Main.maxTilesY - 2)
			{
				Mod.Logger.Error("Y is not alive");
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