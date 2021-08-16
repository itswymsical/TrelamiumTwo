using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;

using TrelamiumTwo.Core;
using Terraria.Audio;

namespace TrelamiumTwo.Content.NPCs.Enemies.Underground
{
	public class SentientGeode : ModNPC
	{
		public override string Texture => Assets.NPCs.Underground + "SentientGeode";
		public override void SetDefaults()
		{
			NPC.damage = 15;
			NPC.defense = 12;
			NPC.lifeMax = 98;
			NPC.width = NPC.height = 36;
			NPC.knockBackResist = 0f;
			NPC.behindTiles = NPC.lavaImmune = true;

			NPC.aiStyle = -1;
			aiType = -1;

			NPC.HitSound = SoundID.NPCHit52;
			NPC.DeathSound = SoundID.NPCDeath52;
			NPC.value = Item.buyPrice(copper: 75);
		}
		public override void AI()
		{
			int num = 30;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			Dust dust22;
			Vector2 position2 = NPC.Center;
			dust22 = Dust.NewDustPerfect(position2, 258, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1.25f);
			dust22.noLight = true;
			dust22.shader = GameShaders.Armor.GetSecondaryShader(114, Main.LocalPlayer);
			dust22.fadeIn = 0.8684211f;

			if (NPC.velocity.Y == 0f && ((NPC.velocity.X > 0f && NPC.direction < 0) || (NPC.velocity.X < 0f && NPC.direction > 0)))
			{
				flag2 = true;
				NPC.ai[3] += 1f;
			}
			int num2 = 4;
			bool flag4 = NPC.velocity.Y == 0f;
			for (int i = 0; i < 200; i++)
			{
				if (i != NPC.whoAmI && Main.npc[i].active && Main.npc[i].type == NPC.type && Math.Abs(NPC.position.X - Main.npc[i].position.X) + Math.Abs(NPC.position.Y - Main.npc[i].position.Y) < (float)NPC.width)
				{
					if (NPC.position.X < Main.npc[i].position.X)
					{
						NPC.velocity.X = NPC.velocity.X - 0.05f;
					}
					else
					{
						NPC.velocity.X = NPC.velocity.X + 0.05f;
					}
					if (NPC.position.Y < Main.npc[i].position.Y)
					{
						NPC.velocity.Y = NPC.velocity.Y - 0.05f;
					}
					else
					{
						NPC.velocity.Y = NPC.velocity.Y + 0.05f;
					}
				}
			}
			if (flag4)
			{
				NPC.velocity.Y = 0f;
			}
			if (NPC.position.X == NPC.oldPosition.X || NPC.ai[3] >= (float)num || flag2)
			{
				NPC.ai[3] += 1f;
				flag3 = true;
			}
			else if (NPC.ai[3] > 0f)
			{
				NPC.ai[3] -= 1f;
			}
			if (NPC.ai[3] > (float)(num * num2))
			{
				NPC.ai[3] = 0f;
			}
			if (NPC.justHit)
			{
				NPC.ai[3] = 0f;
			}
			if (NPC.ai[3] == (float)num)
			{
				NPC.netUpdate = true;
			}
			Vector2 vector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			float num3 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector.X;
			float num4 = Main.player[NPC.target].position.Y - vector.Y;
			float num5 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
			if (num5 < 200f && !flag3)
			{
				NPC.ai[3] = 0f;
			}
			if (NPC.velocity.Y == 0f && Math.Abs(NPC.velocity.X) > 3f && ((NPC.Center.X < Main.player[NPC.target].Center.X && NPC.velocity.X > 0f) || (NPC.Center.X > Main.player[NPC.target].Center.X && NPC.velocity.X < 0f)))
			{
				NPC.velocity.Y = NPC.velocity.Y - 3f;
				if (Main.rand.Next(3) == (0))
				{
					SoundEngine.PlaySound(SoundID.Item, NPC.Center, 14);
				}
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 4, 0f, -1f, 0, default, 1f);
				}
			}
			if (NPC.ai[3] < (float)num)
			{
				NPC.TargetClosest(true);
			}
			else
			{
				if (NPC.velocity.X == 0f)
				{
					if (NPC.velocity.Y == 0f)
					{
						NPC.ai[0] += 1f;
						if (NPC.ai[0] >= 2f)
						{
							NPC.direction *= -1;
							NPC.spriteDirection = NPC.direction;
							NPC.ai[0] = 0f;
						}
					}
				}
				else
				{
					NPC.ai[0] = 0f;
				}
				NPC.directionY = -1;
				if (NPC.direction == 0)
				{
					NPC.direction = 1;
				}
			}
			if (!flag && (NPC.velocity.Y == 0f || NPC.wet || (NPC.velocity.X <= 0f && NPC.direction < 0) || (NPC.velocity.X >= 0f && NPC.direction > 0)))
			{
				if (Math.Sign(NPC.velocity.X) != NPC.direction)
				{
					NPC.velocity.X = NPC.velocity.X * 0.92f;
				}
				float num9 = MathHelper.Lerp(0.6f, 1f, Math.Abs(Main.windSpeedSet)) * (float)Math.Sign(Main.windSpeedSet);
				if (!Main.player[NPC.target].ZoneSandstorm)
				{
					num9 = 0f;
				}
				float num7 = 4f + num9 * (float)NPC.direction * 4f;
				float num8 = 0.1f;
				if (NPC.velocity.X < -num7 || NPC.velocity.X > num7)
				{
					if (NPC.velocity.Y == 0f)
					{
						NPC.velocity *= 0.8f;
					}
				}
				else if (NPC.velocity.X < num7 && NPC.direction == 1)
				{
					NPC.velocity.X = NPC.velocity.X + num8;
					if (NPC.velocity.X > num7)
					{
						NPC.velocity.X = num7;
					}
				}
				else if (NPC.velocity.X > -num7 && NPC.direction == -1)
				{
					NPC.velocity.X = NPC.velocity.X - num8;
					if (NPC.velocity.X < -num7)
					{
						NPC.velocity.X = -num7;
					}
				}
			}
			if (NPC.velocity.Y >= 0f)
			{
				int num10 = 0;
				if (NPC.velocity.X < 0f)
				{
					num10 = -1;
				}
				if (NPC.velocity.X > 0f)
				{
					num10 = 1;
				}
				Vector2 position = NPC.position;
				position.X += NPC.velocity.X;
				int num11 = (int)((position.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 1) * num10)) / 16f);
				int num12 = (int)((position.Y + (float)NPC.height - 1f) / 16f);
				if (Main.tile[num11, num12] == null)
				{
					Main.tile[num11, num12] = new Tile();
				}
				if (Main.tile[num11, num12 - 1] == null)
				{
					Main.tile[num11, num12 - 1] = new Tile();
				}
				if (Main.tile[num11, num12 - 2] == null)
				{
					Main.tile[num11, num12 - 2] = new Tile();
				}
				if (Main.tile[num11, num12 - 3] == null)
				{
					Main.tile[num11, num12 - 3] = new Tile();
				}
				if (Main.tile[num11, num12 + 1] == null)
				{
					Main.tile[num11, num12 + 1] = new Tile();
				}
				if ((float)(num11 * 16) < position.X + (float)NPC.width && (float)(num11 * 16 + 16) > position.X && ((Main.tile[num11, num12].nactive() && !Main.tile[num11, num12].topSlope() && !Main.tile[num11, num12 - 1].topSlope() && Main.tileSolid[(int)Main.tile[num11, num12].type] && !Main.tileSolidTop[(int)Main.tile[num11, num12].type]) || (Main.tile[num11, num12 - 1].halfBrick() && Main.tile[num11, num12 - 1].nactive())) && (!Main.tile[num11, num12 - 1].nactive() || !Main.tileSolid[(int)Main.tile[num11, num12 - 1].type] || Main.tileSolidTop[(int)Main.tile[num11, num12 - 1].type] || (Main.tile[num11, num12 - 1].halfBrick() && (!Main.tile[num11, num12 - 4].nactive() || !Main.tileSolid[(int)Main.tile[num11, num12 - 4].type] || Main.tileSolidTop[(int)Main.tile[num11, num12 - 4].type]))) && (!Main.tile[num11, num12 - 2].nactive() || !Main.tileSolid[(int)Main.tile[num11, num12 - 2].type] || Main.tileSolidTop[(int)Main.tile[num11, num12 - 2].type]) && (!Main.tile[num11, num12 - 3].nactive() || !Main.tileSolid[(int)Main.tile[num11, num12 - 3].type] || Main.tileSolidTop[(int)Main.tile[num11, num12 - 3].type]) && (!Main.tile[num11 - num10, num12 - 3].nactive() || !Main.tileSolid[(int)Main.tile[num11 - num10, num12 - 3].type]))
				{
					float num13 = (float)(num12 * 16);
					if (Main.tile[num11, num12].halfBrick())
					{
						num13 += 8f;
					}
					if (Main.tile[num11, num12 - 1].halfBrick())
					{
						num13 -= 8f;
					}
					if (num13 < position.Y + (float)NPC.height)
					{
						float num14 = position.Y + (float)NPC.height - num13;
						if ((double)num14 <= 16.1)
						{
							NPC.gfxOffY += NPC.position.Y + (float)NPC.height - num13;
							NPC.position.Y = num13 - (float)NPC.height;
							if (num14 < 9f)
							{
								NPC.stepSpeed = 0.5f;
							}
							else
							{
								NPC.stepSpeed = 1f;
							}
						}
					}
				}
			}
			if (NPC.velocity.Y == 0f)
			{
				int num15 = (int)((NPC.position.X + (float)(NPC.width / 2) + (float)((NPC.width / 2 + 2) * NPC.direction) + NPC.velocity.X * 5f) / 16f);
				int num16 = (int)((NPC.position.Y + (float)NPC.height - 15f) / 16f);
				if (Main.tile[num15, num16] == null)
				{
					Main.tile[num15, num16] = new Tile();
				}
				if (Main.tile[num15, num16 - 1] == null)
				{
					Main.tile[num15, num16 - 1] = new Tile();
				}
				if (Main.tile[num15, num16 - 2] == null)
				{
					Main.tile[num15, num16 - 2] = new Tile();
				}
				if (Main.tile[num15, num16 - 3] == null)
				{
					Main.tile[num15, num16 - 3] = new Tile();
				}
				if (Main.tile[num15, num16 + 1] == null)
				{
					Main.tile[num15, num16 + 1] = new Tile();
				}
				if (Main.tile[num15 + NPC.direction, num16 - 1] == null)
				{
					Main.tile[num15 + NPC.direction, num16 - 1] = new Tile();
				}
				if (Main.tile[num15 + NPC.direction, num16 + 1] == null)
				{
					Main.tile[num15 + NPC.direction, num16 + 1] = new Tile();
				}
				if (Main.tile[num15 - NPC.direction, num16 + 1] == null)
				{
					Main.tile[num15 - NPC.direction, num16 + 1] = new Tile();
				}
				int num17 = NPC.spriteDirection;
				num17 *= -1;
				if ((NPC.velocity.X < 0f && num17 == -1) || (NPC.velocity.X > 0f && num17 == 1))
				{
					bool flag6 = NPC.type == NPCID.StardustSpiderSmall || NPC.type == NPCID.NebulaBeast;
					float num18 = 3f;
					if (Main.tile[num15, num16 - 2].nactive() && Main.tileSolid[(int)Main.tile[num15, num16 - 2].type])
					{
						if (Main.tile[num15, num16 - 3].nactive() && Main.tileSolid[(int)Main.tile[num15, num16 - 3].type])
						{
							NPC.velocity.Y = -8.5f;
							NPC.netUpdate = true;
						}
						else
						{
							NPC.velocity.Y = -7.5f;
							NPC.netUpdate = true;
						}
					}
					else if (Main.tile[num15, num16 - 1].nactive() && !Main.tile[num15, num16 - 1].topSlope() && Main.tileSolid[(int)Main.tile[num15, num16 - 1].type])
					{
						NPC.velocity.Y = -7f;
						NPC.netUpdate = true;
					}
					else if (NPC.position.Y + (float)NPC.height - (float)(num16 * 16) > 20f && Main.tile[num15, num16].nactive() && !Main.tile[num15, num16].topSlope() && Main.tileSolid[(int)Main.tile[num15, num16].type])
					{
						NPC.velocity.Y = -6f;
						NPC.netUpdate = true;
					}
					else if ((NPC.directionY < 0 || Math.Abs(NPC.velocity.X) > num18) && (!flag6 || !Main.tile[num15, num16 + 1].nactive() || !Main.tileSolid[(int)Main.tile[num15, num16 + 1].type]) && (!Main.tile[num15, num16 + 2].nactive() || !Main.tileSolid[(int)Main.tile[num15, num16 + 2].type]) && (!Main.tile[num15 + NPC.direction, num16 + 3].nactive() || !Main.tileSolid[(int)Main.tile[num15 + NPC.direction, num16 + 3].type]))
					{
						NPC.velocity.Y = -8f;
						NPC.netUpdate = true;
					}
				}
			}
			NPC.rotation += NPC.velocity.X * 0.05f;
			NPC.spriteDirection = -NPC.direction;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnCondition.Cavern.Chance * 0.1995f;
		
		public override void NPCLoot()
		{
			int choice = Main.rand.Next(7);

			if (choice == 0)
				Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Amethyst, Main.rand.Next(1, 3));

			if (choice == 1)
				Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Topaz, Main.rand.Next(1, 3));

			if (choice == 2)
				Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Sapphire, Main.rand.Next(1, 2));

			if (choice == 3)
				Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Emerald, Main.rand.Next(1, 2));

			if (choice == 4)
				Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Ruby);

			if (choice == 5)
				Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Diamond);

			if (choice == 6)
				Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Amber);

		}
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int k = 0; k < 5; k++)
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 4, hitDirection, -1f, 0, default, 1f);

			if (NPC.life <= 0)
				for (int k = 0; k < 40; k++)
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 4, hitDirection, -1f, 0, default, 1f);
		}
	}
}