using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.NPCs.Enemies.Desert
{
	public class AncientScarab : ModNPC
	{
		public override string Texture => Assets.NPCs.Desert + "AncientScarab";
		public override void SetStaticDefaults() => Main.npcFrameCount[NPC.type] = 4;
		private int frameY;
		public override void SetDefaults()
		{
			NPC.width = 62;
			NPC.height = 50;
			NPC.value = Item.buyPrice(silver: 1);

			NPC.noGravity = true;

			NPC.damage = 17;
			NPC.defense = 4;
			NPC.aiStyle = 44;
			NPC.lifeMax = 124;
			NPC.knockBackResist = 0f;
			
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) => Main.LocalPlayer.ZoneDesert ? 0.15f : 0f;
		public override void FindFrame(int frameHeight)
		{
			NPC.spriteDirection = -NPC.direction;
			NPC.frameCounter++;
			int frameRate = 4;
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
	}
}
