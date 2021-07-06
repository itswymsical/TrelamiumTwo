using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;
using TrelamiumTwo.Helpers;

namespace TrelamiumTwo.Content.NPCs.Enemies.Desert
{
	public class Scarab : ModNPC
	{
		public override string Texture => Assets.NPCs.Desert + "Scarab";
		public override void SetStaticDefaults() => Main.npcFrameCount[npc.type] = 4;
		private int frameY;
		public override void SetDefaults()
		{
			npc.width = 28;
			npc.height = 30;
			npc.value = Item.buyPrice(copper: 30);
			
			npc.damage = 20;
			npc.defense = 10;
			npc.lifeMax = 55;
			npc.knockBackResist = 0.5f;

			npc.aiStyle = -1;
			aiType = -1;

			npc.HitSound = SoundID.NPCHit31;
			npc.DeathSound = SoundID.NPCDeath16;

			drawOffsetY = -2f;
		}
        public override void AI() => npc.GenericFighterAI();
       
		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = -npc.direction;
			int frameRate = 4;
			npc.frameCounter++;
			if (frameY > 2)
			{
				frameY = 0;
			}
			if (npc.frameCounter > frameRate)
			{
				frameY++;

				npc.frameCounter = 0;
			}
			npc.frame.Y = frameY * frameHeight;
		}
	

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life > 0)
			{
				for (int i = 0; i < damage / npc.lifeMax * 100f; ++i)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, DustID.Vile, hitDirection, -1f, npc.alpha, npc.color, npc.scale);
				}
				return;
			}

			for (int i = 1; i <= 2; i++)
				Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Content/Gores/Scarab/ScarabGore" + i));

			for (int i = 0; i < 50; ++i)
				Dust.NewDust(npc.position, npc.width, npc.height, DustID.Vile, hitDirection, -2f, npc.alpha, npc.color, npc.scale);
			
		}
		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Materials.DesolateHusk>(), Main.rand.Next(1, 2));

			if (Main.rand.Next(2) == 0)
				Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Materials.CrackedScarabHorn>());
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnCondition.OverworldDayDesert.Chance * 0.35f;
	}
}
