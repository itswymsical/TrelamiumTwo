using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Helpers;

namespace TrelamiumTwo.Content.NPCs.Critters
{
	public class WoodPecker : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 5;
			Main.npcCatchable[npc.type] = true;
		}
		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.BirdRed);
			npc.friendly = true;

			aiType = NPCID.BirdRed;
			animationType = NPCID.BirdRed;
			npc.catchItem = (short)ModContent.ItemType<Items.Misc.WoodPecker>();
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.player.ZoneForest() && Main.dayTime ? 0.1f : 0;
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
				for (int i = 1; i <= 2; i++)
					Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BirbGore" + i), npc.scale);
		}
	}
}