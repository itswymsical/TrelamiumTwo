using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.NPCs.Enemies.Desert
{
	public class SaharanEagle : ModNPC
	{
        public override string Texture => Assets.NPCs.Desert + "SaharanEagle";
        public override void SetStaticDefaults() => Main.npcFrameCount[npc.type] = 8;
		private int frameY;
		public override void SetDefaults()
		{
			npc.width = 88;			
			npc.height = 74;

			npc.value = Item.buyPrice(silver: 4);

			npc.noGravity = true;

			npc.damage = 19;
			npc.defense = 4;
			npc.aiStyle = 44;
			npc.lifeMax = 190;
			npc.knockBackResist = 0f;
			
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (NPC.downedBoss1) 
				return SpawnCondition.OverworldDayDesert.Chance * 3.95f;
			return 0;
		}
		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = -npc.direction;
			npc.frameCounter++;
			int frameRate = 3;
			if (frameY > 6)
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
	}
}
