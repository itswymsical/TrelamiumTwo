using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Core;
using TrelamiumTwo.Helpers;

namespace TrelamiumTwo.Content.NPCs.Critters
{
	public class WoodPecker : ModNPC
	{
		public override string Texture => Assets.NPCs.Critters + "WoodPecker";
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
		public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnCondition.OverworldDayBirdCritter.Chance * 0.7f;
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
				for (int i = 1; i <= 2; i++)
					Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WoodPecker/WoodPeckerGore" + i), npc.scale);
		}
	}
}