using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TrelamiumTwo.Content.NPCs.Cumulor
{
	public class Cumulor : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cumulor");
			//Main.npcFrameCount[npc.type] = 6;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.width = 184;
			npc.height = 180;

			npc.damage = 0;
			npc.defense = 12;
			npc.lifeMax = 4500;
			npc.noGravity = true;
			npc.boss = true;
			npc.lavaImmune = true;
			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.NPCHit18;
			npc.DeathSound = SoundID.NPCDeath13;
			npc.value = Item.buyPrice(0, 0, 1, 33);
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/FogFulcrum");
		}

		public override void AI()
		{
			npc.ai[1] += 1; //Frame Counter
			npc.ai[2] += 1f; //Fly Spawner
			float posX = npc.position.X;
			float posY = npc.position.Y;
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, 16, hitDirection, -1f, 0, default(Color), 1f);
			}
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, 825, 1f);
				for (int k = 0; k < 25; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, 16, hitDirection, -1f, 0, default(Color), 1f);
				}
				for (int k2 = 0; k2 < 3; k2++)
				{
					Gore.NewGore(npc.position, npc.velocity, 826, Main.rand.NextFloat(1.3f, 1.75f));
				}
			}
		}
	}
}