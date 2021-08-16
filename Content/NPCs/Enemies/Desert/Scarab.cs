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
		public override void SetStaticDefaults() => Main.npcFrameCount[NPC.type] = 4;
		private int frameY;
		public override void SetDefaults()
		{
			NPC.width = 28;
			NPC.height = 30;
			NPC.value = Item.buyPrice(copper: 30);
			
			NPC.damage = 20;
			NPC.defense = 10;
			NPC.lifeMax = 55;
			NPC.knockBackResist = 0.5f;

			NPC.aiStyle = -1;
			aiType = -1;

			NPC.HitSound = SoundID.NPCHit31;
			NPC.DeathSound = SoundID.NPCDeath16;

			drawOffsetY = -2f;
		}
        public override void AI() => NPC.GenericFighterAI();
       
		public override void FindFrame(int frameHeight)
		{
			NPC.spriteDirection = -NPC.direction;
			int frameRate = 4;
			NPC.frameCounter++;
			if (frameY > 2)
			{
				frameY = 0;
			}
			if (NPC.frameCounter > frameRate)
			{
				frameY++;

				NPC.frameCounter = 0;
			}
			NPC.frame.Y = frameY * frameHeight;
		}
	

		public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life > 0)
			{
				for (int i = 0; i < damage / NPC.lifeMax * 100f; ++i)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Vile, hitDirection, -1f, NPC.alpha, NPC.color, NPC.scale);
				}
				return;
			}

			for (int i = 1; i <= 2; i++)
				Gore.NewGore(NPC.Center, NPC.velocity, Mod.GetGoreSlot("Content/Gores/Scarab/ScarabGore" + i));

			for (int i = 1; i <= 2; i++)
				Gore.NewGore(NPC.Center, NPC.velocity, Mod.GetGoreSlot("Content/Gores/Scarab/ScarabGore2"));

			for (int i = 0; i < 50; ++i)
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Vile, hitDirection, -2f, NPC.alpha, NPC.color, NPC.scale);
			
		}
		public override void NPCLoot()
		{
			Item.NewItem(NPC.getRect(), ModContent.ItemType<Items.Materials.DesolateHusk>(), Main.rand.Next(1, 2));

			if (Main.rand.Next(2) == 0)
				Item.NewItem(NPC.getRect(), ModContent.ItemType<Items.Materials.CrackedScarabHorn>());
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) => Main.LocalPlayer.ZoneDesert ? 0.35f : 0f;
	}
}
