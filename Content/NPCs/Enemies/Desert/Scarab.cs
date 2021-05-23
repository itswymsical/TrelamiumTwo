using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Utilities.Extensions;

namespace TrelamiumTwo.Content.NPCs.Enemies.Desert
{
	public class Scarab : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 4;
		}
		public override void SetDefaults()
		{
			npc.width = 28;
			npc.height = 22;
			npc.value = Item.sellPrice(copper: 60);
			
			npc.damage = 20;
			npc.defense = 10;
			npc.lifeMax = 55;
			npc.knockBackResist = 0.5f;

			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath16;

			drawOffsetY = -2f;
		}
        public override void AI()
        {
			npc.GenericFighterAI();
        }
        public override void FindFrame(int frameHeight)
		{
			if (++npc.frameCounter >= 6)
			{
				npc.frameCounter = 0;
				npc.frame.Y = (npc.frame.Y + frameHeight) % (Main.npcFrameCount[npc.type] * frameHeight);
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life > 0)
			{
				for (int i = 0; i < damage / npc.lifeMax * 100f; ++i)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, 18, hitDirection, -1f, npc.alpha, npc.color, npc.scale);
				}
				return;
			}

			for (int i = 0; i < 50; ++i)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, 18, hitDirection, -2f, npc.alpha, npc.color, npc.scale);
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
			=> SpawnCondition.DesertCave.Chance * 0.35f;
	}
}
