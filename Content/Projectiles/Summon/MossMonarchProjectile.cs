using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace Trelamium2.Content.Projectiles.Summon
{
	public class MossMonarchProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moss Monarch");
		}
		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.penetrate = -1;
			projectile.timeLeft *= 5;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.minion = true;
			projectile.minionSlots = 1f;
		}
		public override void AI()
		{
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 8)
			{
				projectile.frameCounter = 0;
				num = projectile.frame + 1;
				projectile.frame = num;
				if (num >= 6)
				{
					projectile.frame = 0;
				}
			}
			var modPlayer = Main.LocalPlayer.GetModPlayer<Common.Players.TrelamiumPlayer>();
			Player player = Main.player[projectile.owner];
			player.AddBuff(BuffType<Buffs.Minion.MossMonarchBuff>(), 3600);

			if (player.dead)
			{
				modPlayer.mossMonarch = false;
			}
			if (modPlayer.mossMonarch)
			{
				projectile.timeLeft = 2;
			}
			float num8 = 0.01f;
			float num9 = (float)projectile.width;
			num9 *= 2f;
			for (int j = 0; j < 1000; j++)
			{
				if (j != projectile.whoAmI && Main.projectile[j].active && Main.projectile[j].owner == projectile.owner && Main.projectile[j].type == projectile.type && Math.Abs(projectile.position.X - Main.projectile[j].position.X) + Math.Abs(projectile.position.Y - Main.projectile[j].position.Y) < num9)
				{
					if (projectile.position.X < Main.projectile[j].position.X)
					{
						projectile.velocity.X = projectile.velocity.X - num8;
					}
					else
					{
						projectile.velocity.X = projectile.velocity.X + num8;
					}
					if (projectile.position.Y < Main.projectile[j].position.Y)
					{
						projectile.velocity.Y = projectile.velocity.Y - num8;
					}
					else
					{
						projectile.velocity.Y = projectile.velocity.Y + num8;
					}
				}
			}
			Vector2 vector = projectile.position;
			float num10 = 400f;
			bool flag = false;
			int num11 = -1;
			projectile.tileCollide = true;

			projectile.tileCollide = false;
			if (Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
			{
				projectile.alpha += 20;
				if (projectile.alpha > 150)
				{
					projectile.alpha = 150;
				}
			}
			else
			{
				projectile.alpha -= 50;
				if (projectile.alpha < 60)
				{
					projectile.alpha = 60;
				}
			}


			Vector2 center = Main.player[projectile.owner].Center;
			Vector2 value = new Vector2(0.5f);
			NPC ownerMinionAttackTargetNPC = projectile.OwnerMinionAttackTargetNPC;
			if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(projectile, false))
			{
				Vector2 vector2 = ownerMinionAttackTargetNPC.position + ownerMinionAttackTargetNPC.Size * value;
				float num12 = Vector2.Distance(vector2, center);
				if (((Vector2.Distance(center, vector) > num12 && num12 < num10) || !flag) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, ownerMinionAttackTargetNPC.position, ownerMinionAttackTargetNPC.width, ownerMinionAttackTargetNPC.height))
				{
					num10 = num12;
					vector = vector2;
					flag = true;
					num11 = ownerMinionAttackTargetNPC.whoAmI;
				}
			}
			if (!flag)
			{
				for (int k = 0; k < 200; k++)
				{
					NPC nPC = Main.npc[k];
					if (nPC.CanBeChasedBy(projectile, false))
					{
						Vector2 vector3 = nPC.position + nPC.Size * value;
						float num13 = Vector2.Distance(vector3, center);
						if (((Vector2.Distance(center, vector) > num13 && num13 < num10) || !flag) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, nPC.position, nPC.width, nPC.height))
						{
							num10 = num13;
							vector = vector3;
							flag = true;
							num11 = k;
						}
					}
				}
			}

			int num16 = 500;
			if (flag)
			{
				num16 = 1000;
			}
			if (Vector2.Distance(player.Center, projectile.Center) > (float)num16)
			{
				projectile.ai[0] = 1f;
				projectile.netUpdate = true;
			}
			if (projectile.ai[0] == 1f)
			{
				projectile.tileCollide = false;
			}
			if (flag && projectile.ai[0] == 0f)
			{
				Vector2 vector4 = vector - projectile.Center;
				float num17 = vector4.Length();
				vector4.Normalize();
				if (num17 > 400f)
				{
					float scaleFactor = 3f;
					vector4 *= scaleFactor;
					projectile.velocity = (projectile.velocity * 20f + vector4) / 21f;
				}
				else
				{
					projectile.velocity *= 0.96f;
				}

				if (num17 > 200f)
				{
					float scaleFactor2 = 6f;
					vector4 *= scaleFactor2;
					projectile.velocity.X = (projectile.velocity.X * 40f + vector4.X) / 41f;
					projectile.velocity.Y = (projectile.velocity.Y * 40f + vector4.Y) / 41f;
				}
				else if (projectile.velocity.Y > -1f)
				{
					projectile.velocity.Y = projectile.velocity.Y - 0.1f;
				}
			}
			else
			{
				if (!Collision.CanHitLine(projectile.Center, 1, 1, Main.player[projectile.owner].Center, 1, 1))
				{
					projectile.ai[0] = 1f;
				}
				float num21 = 7f;
				Vector2 center2 = projectile.Center;
				Vector2 vector6 = player.Center - center2 + new Vector2(0f, -60f);
				vector6 += new Vector2(0f, 40f);
				float num23 = vector6.Length();
				if (num23 > 200f && num21 < 9f)
				{
					num21 = 9f;
				}
				if (num23 < 100f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
				{
					projectile.ai[0] = 0f;
					projectile.netUpdate = true;
				}
				if (num23 > 2000f)
				{
					projectile.position.X = Main.player[projectile.owner].Center.X - (float)(projectile.width / 2);
					projectile.position.Y = Main.player[projectile.owner].Center.Y - (float)(projectile.width / 2);
				}

				if (Math.Abs(vector6.X) > 40f || Math.Abs(vector6.Y) > 10f)
				{
					vector6.Normalize();
					vector6 *= num21;
					vector6 *= new Vector2(1.25f, 0.65f);
					projectile.velocity = (projectile.velocity * 20f + vector6) / 21f;
				}
				else
				{
					if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
					{
						projectile.velocity.X = -0.15f;
						projectile.velocity.Y = -0.05f;
					}
					projectile.velocity *= 1.01f;
				}

				if (num23 > 70f)
				{
					vector6.Normalize();
					vector6 *= num21;
					projectile.velocity = (projectile.velocity * 20f + vector6) / 21f;
				}
				else
				{
					if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
					{
						projectile.velocity.X = -0.15f;
						projectile.velocity.Y = -0.05f;
					}
					projectile.velocity *= 1.01f;
				}
			}
			projectile.rotation = projectile.velocity.X * 0.05f;
			if (projectile.velocity.X > 0f)
			{
				projectile.spriteDirection = (projectile.direction = -1);
			}
			else if (projectile.velocity.X < 0f)
			{
				projectile.spriteDirection = (projectile.direction = 1);
			}

			if (projectile.ai[1] > 0f)
			{
				projectile.ai[1] += 1f;
				if (Main.rand.Next(3) != 0)
				{
					projectile.ai[1] += 1f;
				}
			}
			if (projectile.ai[1] > 50f)
			{
				projectile.ai[1] = 0f;
				projectile.netUpdate = true;
			}
			if (projectile.ai[0] == 0f)
			{
				float scaleFactor4 = 8f;
				int type = Main.rand.Next(ProjectileID.VortexBeater, ProjectileID.VortexBeaterRocket);
				if (flag)
				{
					if (!Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
					{
						if (projectile.ai[1] == 0f)
						{
							Vector2 vector9 = vector - projectile.Center;
							projectile.ai[1] += 1f;
							if (Main.myPlayer == projectile.owner)
							{
								vector9.Normalize();
								vector9 *= scaleFactor4;
								int num32 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector9.X, vector9.Y, type, projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
								Main.projectile[num32].friendly = true;
								Main.projectile[num32].netUpdate = true;
								projectile.netUpdate = true;
							}
						}
					}
				}
			}
		}
	}
}