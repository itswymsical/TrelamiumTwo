#region Using directives

using System;
using System.IO;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

#endregion

namespace TrelamiumTwo.Content.NPCs.Enemies.Desert
{
	internal sealed class Scarab : ModNPC
	{
		private readonly float maxSpeed = 1.5f;

		/// <summary>
		/// Determines the amount of time (in ticks/frames) the NPC can remain 'stuck' in place, before attempting to move away.
		/// </summary>
		private readonly int maxAllowedStuckTime = 60;

		private bool IsStuck => npc.ai[3] >= maxAllowedStuckTime;
		
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 4;
		}
		public override void SetDefaults()
		{
			npc.width = 28;
			npc.height = 22;
			npc.value = Item.sellPrice(copper: 60);
			
			npc.damage = 20;
			npc.defense = 10;
			npc.lifeMax = 55;
			npc.knockBackResist = 0.5f;

			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath16;

			drawOffsetY = -2f;
		}

		public override bool PreAI()
		{
			bool canJump = false;
			if (npc.velocity.X == 0f && !npc.justHit)
			{
				canJump = true;
			}

			bool flag4 = false;

			if (npc.velocity.Y == 0f && Math.Sign(npc.velocity.X) != npc.direction)
			{
				flag4 = true;
			}

			if (npc.position.X == npc.oldPosition.X || IsStuck || flag4)
			{
				npc.ai[3]++;
			}
			else if (Math.Abs(npc.velocity.X) > 0.9f && npc.ai[3] > 0f)
			{
				npc.ai[3]--;
			}

			if (npc.ai[3] > maxAllowedStuckTime * 10)
			{
				npc.ai[3] = 0f;
			}

			if (npc.justHit)
			{
				npc.ai[3] = 0f;
			}

			if (IsStuck)
			{
				// First update being stuck.
				if (npc.ai[3] == maxAllowedStuckTime)
				{
					npc.netUpdate = true;
				}

				if (npc.velocity.X == 0f)
				{
					if (npc.velocity.Y == 0f)
					{
						if (++npc.ai[0] >= 2f)
						{
							npc.ai[0] = 0f;
							npc.direction *= -1;
							npc.spriteDirection = npc.direction;
						}
					}
				}
				else
				{
					npc.ai[0] = 0f;
				}
				if (npc.direction == 0)
				{
					npc.direction = 1;
				}
			}
			else if (npc.ai[3] < maxAllowedStuckTime)
			{
				npc.TargetClosest();
			}

			if (npc.velocity.X < -maxSpeed || npc.velocity.X > maxSpeed)
			{
				if (npc.velocity.Y == 0f)
				{
					npc.velocity *= 0.8f;
				}
			}
			else
			{
				if (npc.velocity.X < maxSpeed && npc.direction == 1)
				{
					npc.velocity.X += 0.07f;
				}
				if (npc.velocity.X > -maxSpeed && npc.direction == -1)
				{
					npc.velocity.X -= 0.07f;
				}
				npc.velocity.X = MathHelper.Clamp(npc.velocity.X , - maxSpeed, maxSpeed);
			}

			bool collisionBottom = false;
			if (npc.velocity.Y == 0f)
			{
				int tileY = (int)(npc.position.Y + npc.height + 7f) / 16;
				int minTileX = (int)(npc.position.X / 16);
				int maxTileX = (int)(npc.position.X + npc.width) / 16;

				for (int tileX = minTileX; tileX <= maxTileX; tileX++)
				{
					if (Main.tile[tileX, tileY] == null)
					{
						return (false);
					}
					if (Main.tile[tileX, tileY].nactive() && Main.tileSolid[Main.tile[tileX, tileY].type])
					{
						collisionBottom = true;
						break;
					}
				}
			}

			if (npc.velocity.Y >= 0f)
			{
				SlopedCollision();
			}

			if (collisionBottom)
			{
				ApplyJump(canJump);
			}

			return (false);
		}

		public override void FindFrame(int frameHeight)
		{
			if (npc.velocity.Y == 0f)
			{
				npc.spriteDirection = -npc.direction;
			}

			if (++npc.frameCounter >= 6)
			{
				npc.frameCounter = 0;
				npc.frame.Y = (npc.frame.Y + frameHeight) % (Main.npcFrameCount[npc.type] * frameHeight);
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life > 0)
			{
				for (int i = 0; i < damage / npc.lifeMax * 100f; ++i)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, 18, hitDirection, -1f, npc.alpha, npc.color, npc.scale);
				}
				return;
			}

			for (int i = 0; i < 50; ++i)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, 18, hitDirection, -2f, npc.alpha, npc.color, npc.scale);
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
			=> SpawnCondition.OverworldDay.Chance * 0.3f;

		private void SlopedCollision()
		{
			int velocityDirection = Math.Sign(npc.velocity.X);
			Vector2 targetPosition = npc.position + new Vector2(npc.velocity.X, 0);

			int tileX = (int)((targetPosition.X + (npc.width / 2) + ((npc.width / 2 + 1) * velocityDirection)) / 16f);
			int tileY = (int)((targetPosition.Y + npc.height - 1f) / 16f);

			Tile tile1 = Framing.GetTileSafely(tileX, tileY);
			Tile tile2 = Framing.GetTileSafely(tileX, tileY - 1);
			Tile tile3 = Framing.GetTileSafely(tileX, tileY - 2);
			Tile tile4 = Framing.GetTileSafely(tileX, tileY - 3);
			Tile tile5 = Framing.GetTileSafely(tileX, tileY - 4);
			Tile tile6 = Framing.GetTileSafely(tileX - velocityDirection, tileY - 3);

			if (tileX * 16 < targetPosition.X + npc.width && tileX * 16 + 16 > targetPosition.X &&
				((tile1.nactive() && !tile1.topSlope() && !tile2.topSlope() && Main.tileSolid[tile1.type] && !Main.tileSolidTop[tile1.type]) ||
				(tile2.halfBrick() && tile2.nactive())) && (!tile2.nactive() || !Main.tileSolid[tile2.type] || Main.tileSolidTop[tile2.type] ||
				(tile2.halfBrick() &&
				(!tile5.nactive() || !Main.tileSolid[tile5.type] || Main.tileSolidTop[tile5.type]))) &&
				(!tile3.nactive() || !Main.tileSolid[tile3.type] || Main.tileSolidTop[tile3.type]) &&
				(!tile4.nactive() || !Main.tileSolid[tile4.type] || Main.tileSolidTop[tile4.type]) &&
				(!tile6.nactive() || !Main.tileSolid[tile6.type]))
			{
				float tileYPosition = tileY * 16;
				if (Main.tile[tileX, tileY].halfBrick())
				{
					tileYPosition += 8f;
				}
				if (Main.tile[tileX, tileY - 1].halfBrick())
				{
					tileYPosition -= 8f;
				}

				if (tileYPosition < targetPosition.Y + npc.height)
				{
					float targetYPosition = targetPosition.Y + npc.height - tileYPosition;
					if (targetYPosition <= 16.1f)
					{
						npc.gfxOffY += npc.position.Y + npc.height - tileYPosition;
						npc.position.Y = tileYPosition - npc.height;

						if (targetYPosition < 9f)
						{
							npc.stepSpeed = 1f;
						}
						else
						{
							npc.stepSpeed = 2f;
						}
					}
				}
			}
		}

		/// <summary>
		/// Try to apply a 'jumping' velocity to the current NPC, based on the current state and position of said NPC.
		/// </summary>
		/// <param name="canJump">Pre-defined variable to see if the NPC can attempt a jump.</param>
		private void ApplyJump(bool canJump)
		{
			int tileX = (int)((npc.Center.X + 15 * npc.direction) / 16f);
			int tileY = (int)((npc.position.Y + npc.height - 15f) / 16f);

			Tile tile1 = Framing.GetTileSafely(tileX, tileY);
			Tile tile2 = Framing.GetTileSafely(tileX, tileY - 1);
			Tile tile3 = Framing.GetTileSafely(tileX, tileY + 1);
			Tile tile4 = Framing.GetTileSafely(tileX + npc.direction, tileY + 1);

			tile3.halfBrick();

			if (npc.spriteDirection == Math.Sign(npc.velocity.X))
			{
				if (tile2.nactive() && Main.tileSolid[tile2.type])
				{
					npc.netUpdate = true;
					npc.velocity.Y = -6f;
				}
				else if (npc.position.Y + npc.height - (tileY * 16) > 20f && tile1.nactive() && !tile1.topSlope() && Main.tileSolid[tile1.type])
				{
					npc.netUpdate = true;
					npc.velocity.Y = -5f;
				}
				else if (npc.directionY < 0 &&
					(!tile3.nactive() || !Main.tileSolid[tile3.type]) &&
					(!tile4.nactive() || !Main.tileSolid[tile4.type]))
				{
					npc.netUpdate = true;
					npc.velocity.Y = -8f;
					npc.velocity.X *= 1.5f;
				}

				if (npc.velocity.Y == 0f && canJump && npc.ai[3] == 1f)
				{
					npc.velocity.Y = -5f;
				}
			}

			// Dash attack.
			Vector2 directionToTarget = Main.player[npc.target].Center - npc.Center;
			if (npc.velocity.Y == 0f && Math.Abs(directionToTarget.X) < 100f &&	Math.Abs(directionToTarget.Y) < 50f &&
				((npc.direction > 0 && npc.velocity.X >= 1f) || (npc.direction < 0 && npc.velocity.X <= -1f)))
			{
				npc.velocity.X = MathHelper.Clamp(npc.velocity.X * 2, -3, 3);
				npc.velocity.Y = -4f;
				npc.netUpdate = true;
			}
		}
	}
}
