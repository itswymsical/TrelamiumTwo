using TrelamiumTwo.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.NPCs.Enemies.Overworld
{
	public class Mushbringer : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mushnib");

			Main.npcFrameCount[npc.type] = 3;
		}

		public override void SetDefaults()
		{
			npc.lifeMax = 10;

			npc.width = npc.height = 34;

			npc.aiStyle = -1;
			aiType = -1;

			npc.knockBackResist = 0.6f;

			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
		}

		private int frame;

		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;

			if (npc.velocity.Y == 0f)
				frame = 0;
			else if (npc.velocity.Y < 0f)
				frame = 1;

			if (npc.life < npc.lifeMax)
				frame = 2;
	
			npc.frame.Y = frame * frameHeight;
		}

		public override void AI()
		{
			if (npc.life == npc.lifeMax)
			{
				npc.rotation = npc.velocity.X * 0.1f;

				if (npc.velocity.Y == 0f)
					npc.ai[0]++;
				else
					npc.ai[0] = 0f;

				if (npc.ai[0] >= 120f)
				{
					npc.velocity.Y = Main.rand.NextFloat(-8f, -6f);

					npc.direction = Main.rand.NextBool() ? -1 : 1;

					npc.netUpdate = true;
				}

				if (npc.velocity.Y < 0f)
					npc.velocity.X += npc.direction * 0.1f;
				else
					npc.velocity.X *= 0.95f;
			}
			else
			{
				npc.velocity.X *= 0.95f;

				npc.ai[1]++;

				if (npc.ai[1] >= 30f)
				{
					int[] types =
					{
						ProjectileID.SporeGas,
						ProjectileID.SporeGas2,
						ProjectileID.SporeGas3
					};

					var velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));

					Projectile.NewProjectile(npc.Center, velocity, types[Main.rand.Next(3)], npc.damage / 2, 1f);

					npc.ai[1] = 0f;

					npc.netUpdate = true;
				}
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			int[] types =
			{
				ProjectileID.SporeGas,
				ProjectileID.SporeGas2,
				ProjectileID.SporeGas3
			};

			if (npc.life <= 0)
			{
				for (int i = 0; i < Main.rand.Next(2, 5); i++)
				{
					var velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));

					Projectile.NewProjectile(npc.Center, velocity, types[Main.rand.Next(3)], npc.damage / 2, 1f);

					npc.netUpdate = true;
				}
			}
			else
			{
				var velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));

				Projectile.NewProjectile(npc.Center, velocity, types[Main.rand.Next(3)], npc.damage / 2, 1f);
			}

			npc.netUpdate = true;
		}
	}
}
