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
			Main.npcFrameCount[NPC.type] = 5;
			Main.npcCatchable[NPC.type] = true;
		}
		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.BirdRed);
			NPC.friendly = true;

			aiType = NPCID.BirdRed;
			animationType = NPCID.BirdRed;
			NPC.catchItem = (short)ModContent.ItemType<Items.Misc.WoodPecker>();
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnCondition.OverworldDayBirdCritter.Chance * 0.7f;
		public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life <= 0)
				for (int i = 1; i <= 2; i++)
					Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/WoodPecker/WoodPeckerGore" + i), NPC.scale);
		}
	}
}