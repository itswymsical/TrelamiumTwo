using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TrelamiumTwo.Content.NPCs.Enemies.Corruption
{
	public class PestilenceHive : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pestilence Hive");
			Main.npcFrameCount[npc.type] = 6;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.width = 46;
			npc.height = 46;

			npc.damage = 0;
			npc.defense = 5;
			npc.lifeMax = 180;

			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.NPCHit18;
			npc.DeathSound = SoundID.NPCDeath13;
			npc.value = Item.buyPrice(0, 0, 1, 33);
		}

		public override void AI()
		{
			npc.ai[1] += 1; //Frame Counter
			npc.ai[2] += 1f; //Fly Spawner
			float posX = npc.position.X;
			float posY = npc.position.Y;
			if (npc.ai[2] == 120)
            {
				Main.PlaySound(4, -1, -1, 13, 0.25f, 0.5f);
				Main.PlaySound(SoundID.NPCDeath12, npc.position);
				for (int a = 0; a < Main.rand.Next(2, 3); a++)
                {
					NPC.NewNPC((int)posX + Main.rand.Next(1, 12), (int)posY + Main.rand.Next(1, 12), NPCType<PestilenceFly>());
                }
			}
			if (npc.ai[1] > 24)
			{
				npc.ai[1] = 0;
			}
			if (npc.ai[2] > 120)
            {
				npc.ai[2] = 0;
            }
		}
		public override void FindFrame(int frameHeight)
		{
			if (npc.ai[1] == 0)
			{
				npc.frame.Y = 0 * frameHeight;
			}
			if (npc.ai[1] == 4)
			{
				npc.frame.Y = 1 * frameHeight;
			}
			if (npc.ai[1] == 8)
			{
				npc.frame.Y = 2 * frameHeight;
			}
			if (npc.ai[1] == 12)
			{
				npc.frame.Y = 3 * frameHeight;
			}
			if (npc.ai[1] == 16)
			{
				npc.frame.Y = 4 * frameHeight;
			}
			if (npc.ai[1] == 24)
			{
				npc.frame.Y = 5 * frameHeight;
			}
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.hardMode)
			{
				return SpawnCondition.Corruption.Chance * 0.235f;
			}
			else return 0f;
		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.45f * bossLifeScale);
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, 18, hitDirection, -1f, 0, default(Color), 1f);
			}
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Content/Gores/PestilenceHive/PestilenceHiveGore1"), 1f);
				for (int k = 0; k < 25; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, 18, hitDirection, -1f, 0, default(Color), 1f);
				}
				for (int k2 = 0; k2 < 3; k2++)
				{
					Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Content/Gores/PestilenceHive/PestilenceHiveGore2"), Main.rand.NextFloat(0.3f, 0.75f));
				}
			}
		}
	}
}