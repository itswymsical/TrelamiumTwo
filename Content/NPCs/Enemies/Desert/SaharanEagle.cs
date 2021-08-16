using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.NPCs.Enemies.Desert
{
	public class SaharanEagle : ModNPC
	{
        public override string Texture => Assets.NPCs.Desert + "SaharanEagle";
        public override void SetStaticDefaults() => Main.npcFrameCount[NPC.type] = 8;
		private int frameY;
		public override void SetDefaults()
		{
			NPC.width = 88;			
			NPC.height = 74;

			NPC.value = Item.buyPrice(silver: 4);

			NPC.noGravity = true;

			NPC.damage = 19;
			NPC.defense = 4;
			NPC.aiStyle = 44;
			NPC.lifeMax = 190;
			NPC.knockBackResist = 0f;
			
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (NPC.downedBoss1) 
				return SpawnCondition.OverworldDayDesert.Chance * 3.95f;
			return 0;
		}
		public override void FindFrame(int frameHeight)
		{
			NPC.spriteDirection = -NPC.direction;
			NPC.frameCounter++;
			int frameRate = 3;
			if (frameY > 6)
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
	}
}
