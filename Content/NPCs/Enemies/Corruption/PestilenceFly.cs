using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace TrelamiumTwo.Content.NPCs.Enemies.Corruption
{
	public class PestilenceFly : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pestilence Fly");
			Main.npcFrameCount[npc.type] = 2;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = 5;
			aiType = NPCID.BeeSmall;
			npc.width = 12;
			npc.height = 12;

			npc.damage = 4;
			npc.defense = 0;
			npc.lifeMax = 14;

			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.NPCHit9;
			npc.DeathSound = SoundID.NPCDeath12;
			npc.value = Item.buyPrice(0, 0, 0, 2);
		}

		public override void AI()
		{
			npc.ai[1] += 1; //Frame Counter
			if (npc.ai[1] > 4)
			{
				npc.ai[1] = 0;
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
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, 18, hitDirection, -1f, 0, default(Color), 1f);
			}
			if (npc.life <= 0)
			{
				for (int k = 0; k < 18; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, 18, hitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
	}
}