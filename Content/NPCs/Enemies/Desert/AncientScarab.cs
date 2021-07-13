using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.NPCs.Enemies.Desert
{
	public class AncientScarab : ModNPC
	{
		public override string Texture => Assets.NPCs.Desert + "AncientScarab";
		public override void SetStaticDefaults() => Main.npcFrameCount[npc.type] = 4;
		private int frameY;
		public override void SetDefaults()
		{
			npc.width = 62;
			npc.height = 50;
			npc.value = Item.buyPrice(silver: 1);

			npc.noGravity = true;

			npc.damage = 17;
			npc.defense = 4;
			npc.aiStyle = 44;
			npc.lifeMax = 124;
			npc.knockBackResist = 0f;
			
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) => Main.LocalPlayer.ZoneDesert ? 0.15f : 0f;
		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = -npc.direction;
			npc.frameCounter++;
			int frameRate = 4;
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
	}
}
