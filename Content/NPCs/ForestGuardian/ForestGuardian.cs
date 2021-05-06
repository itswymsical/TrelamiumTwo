﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using TrelamiumTwo.Core.Utils;
using static Terraria.ModLoader.ModContent;

namespace TrelamiumTwo.Content.NPCs.ForestGuardian
{
	public class ForestGuardian : ModNPC
	{
        #region AIState
        private enum AIState
		{
			Idle = 0,
			Smash = 1
		}
		private AIState State
		{
			get => (AIState)npc.ai[0];
			set => npc.ai[0] = (int)value;
		}
        #endregion
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Forest Guardian");
			NPCID.Sets.TrailCacheLength[npc.type] = 10;
			NPCID.Sets.TrailingMode[npc.type] = 0;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.width = 200;
			npc.height = 184;

			npc.damage = 0;
			npc.defense = 12;
			npc.lifeMax = 1800;

			npc.noTileCollide = true;
			npc.noGravity = true;
			npc.boss = true;
			npc.lavaImmune = true;

			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.NPCHit18;
			npc.DeathSound = SoundID.NPCDeath13;
			npc.value = Item.buyPrice(0, 0, 1, 25);
		}
		bool spawn;
		public int spawnTimer;
		private bool segmentsSpawned;
		public override void AI()
		{
			Player target = Main.player[npc.target];
			spawnTimer++;
			if (target.dead || !target.active)
			{
				npc.TargetClosest(false);
				npc.velocity = new Vector2(0f, -10f);
				npc.timeLeft--;
				if (npc.timeLeft <= 0)
				{
					npc.active = false;
				}
			}

			if (!spawn)
			{
				var Left = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<ForestGuardian_Left>());
				Main.npc[Left].ai[0] = npc.whoAmI;
				var Right = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<ForestGuardian_Right>());
				Main.npc[Right].ai[0] = npc.whoAmI;
				spawn = true;
			}

			if (State == AIState.Idle)
            {
				Movement();
				if (!segmentsSpawned)
				{
					int Index = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<Boulder>());
					Main.npc[Index].ai[0] = npc.whoAmI;
					int Index1 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<Boulder>());
					Main.npc[Index1].ai[0] = npc.whoAmI;
					Main.npc[Index1].ai[1] = 45;
					int Index2 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<Boulder>());
					Main.npc[Index2].ai[0] = npc.whoAmI;
					Main.npc[Index2].ai[1] = 90;
					int Index3 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<Boulder>());
					Main.npc[Index3].ai[0] = npc.whoAmI;
					Main.npc[Index3].ai[1] = 135;
					int Index4 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<Boulder>());
					Main.npc[Index4].ai[0] = npc.whoAmI;
					Main.npc[Index4].ai[1] = 180;
					segmentsSpawned = true;
				}
			}				
			if (State == AIState.Smash)
			{

			}
		}
		private void Movement()
		{
			npc.noGravity = true;
			npc.TargetClosest(true);
			float num697 = 6.5f;
			float num698 = 0.50f;
			Vector2 vector90 = new Vector2(npc.Center.X, npc.Center.Y);
			float num699 = Main.player[npc.target].Center.X - vector90.X;
			float num700 = Main.player[npc.target].Center.Y - vector90.Y - 200f;
			float num701 = (float)Math.Sqrt((num699 * num699 + num700 * num700));
			if (num701 < 20f)
			{
				num699 = npc.velocity.X;
				num700 = npc.velocity.Y;
			}
			else
			{
				num701 = num697 / num701;
				num699 *= num701;
				num700 *= num701;
			}
			if (npc.velocity.X < num699)
			{
				npc.velocity.X = npc.velocity.X + num698;
				if (npc.velocity.X < 0f && num699 > 0f)
				{
					npc.velocity.X = npc.velocity.X + num698 * 2f;
				}
			}
			else if (npc.velocity.X > num699)
			{
				npc.velocity.X = npc.velocity.X - num698;
				if (npc.velocity.X > 0f && num699 < 0f)
				{
					npc.velocity.X = npc.velocity.X - num698 * 2f;
				}
			}
			if (npc.velocity.Y < num700)
			{
				npc.velocity.Y = npc.velocity.Y + num698;
				if (npc.velocity.Y < 0f && num700 > 0f)
				{
					npc.velocity.Y = npc.velocity.Y + num698 * 2f;
				}
			}
			else if (npc.velocity.Y > num700)
			{
				npc.velocity.Y = npc.velocity.Y - num698;
				if (npc.velocity.Y > 0f && num700 < 0f)
				{
					npc.velocity.Y = npc.velocity.Y - num698 * 2f;
				}
			}
		}
		#region Draw Code
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			int alpha = (int)npc.ai[1] / 4;
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (npc.spriteDirection == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Color color25 = Lighting.GetColor((int)(npc.position.X + npc.width * 0.5) / 16, (int)((npc.position.Y + npc.height * 0.5) / 16.0));
			Texture2D texture2D3 = Main.npcTexture[npc.type];

			Rectangle rectangle = new Rectangle(0, 0, texture2D3.Width, texture2D3.Height);
			Vector2 origin2 = rectangle.Size() / 2f;
			Color color29 = npc.GetAlpha(color25);
			Main.spriteBatch.Draw(texture2D3, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY),
				new Rectangle?(rectangle), color29, npc.rotation, origin2, npc.scale, spriteEffects, 0f);
			Color color = new Color(69, 80, 90, alpha / 2);
			for (int k = 0; k < npc.oldPos.Length; k++)
			{
				float scale = npc.scale * (npc.oldPos.Length - k) / npc.oldPos.Length * 1f;
				SpriteBatch spriteBatch1 = Main.spriteBatch;
				Texture2D texture = Main.npcTexture[npc.type];
				Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + origin2;
				Vector2 rotation = npc.rotation.ToRotationVector2();
				double rotAngle = 1.57079637f * k;
				Vector2 center = default;
				spriteBatch1.Draw(texture, drawPos + rotation.RotatedBy(rotAngle, center) * 4f,
									new Rectangle?(rectangle), color, npc.rotation, origin2, scale, spriteEffects, 0f);
			}
			return false;
		}
		public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (spawnTimer < 360)
			{
				var screenCenter = new Vector2(Main.screenWidth, Main.screenHeight) / 2f;
				var fontScale = 0.8f;
				var namePosition = new Vector2(screenCenter.X, 100f);
				string[] names =
				{
					"-- Forest Guardian --",
					"-- Protector of Ancient History --"
				};

				foreach (var name in names)
					DrawUtils.DrawTextCollumn(spriteBatch, lightColor, name, ref namePosition, fontScale);
			}
			int alpha = (int)npc.ai[1] / 4;
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (npc.spriteDirection == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Color color25 = Lighting.GetColor((int)(npc.position.X + npc.width * 0.5) / 16, (int)((npc.position.Y + npc.height * 0.5) / 16.0));
			Texture2D texture2D3 = Main.npcTexture[npc.type];
			Rectangle rectangle = new Rectangle(0, 0, texture2D3.Width, texture2D3.Height);
			Vector2 origin2 = rectangle.Size() / 2f;
			int num155 = 0;
			int num156 = 1;
			int num157 = 1;
			float value3 = 0.5f;
			float num158 = 0f;
			int num159 = num157;
			while ((num156 > 0 && num159 < num155) || (num156 < 0 && num159 > num155))
			{
				Color color26 = color25;
				color26 = npc.GetAlpha(color26);
				if (!(npc.oldPos[num159] == Vector2.Zero))
				{
					float num161 = num159 / num157;
					if (num161 < 0.5f)
					{
						color26 = Color.Lerp(color26, new Color(69, 80, 90, alpha / 2), Utils.InverseLerp(0f, 0.5f, num161, false));
						goto IL_65CC;
					}
					color26 = Color.Lerp(new Color(69, 80, 90, alpha / 2), Color.Cyan, Utils.InverseLerp(0.5f, 1f, num161, false));
					goto IL_65CC;
				}
				else
				{
					goto IL_65CC;
				}
			IL_65B4:
				num159 += num156;
				continue;
			IL_65CC:
				float num162 = num155 - num159;
				if (num156 < 0)
				{
					num162 = num157 - num159;
				}

				color26 *= num162 / NPCID.Sets.TrailCacheLength[npc.type] * 1.5f;
				Vector2 oldPos = npc.oldPos[num159];
				float rotation = npc.rotation;
				SpriteEffects effects = spriteEffects;
				Main.spriteBatch.Draw(texture2D3, oldPos + npc.Size / 2f - Main.screenPosition + new Vector2(0f, npc.gfxOffY), new Rectangle?(rectangle),
					color26, rotation + npc.rotation * num158 * (num159 - 1) * -spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(),
					origin2, MathHelper.Lerp(npc.scale, value3, num159 / 15f), effects, 0f);
				goto IL_65B4;
			}

			Color color = new Color(69, 80, 90, alpha / 2);
			for (int k = 0; k < 4; k++)
			{
				SpriteBatch spriteBatch1 = Main.spriteBatch;
				Texture2D texture = Main.npcTexture[npc.type];
				Vector2 drawPos = npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY);
				Vector2 rotation = npc.rotation.ToRotationVector2();
				double rotAngle = 1.57079637f * k;
				Vector2 center = default;
				spriteBatch1.Draw(texture, drawPos + rotation.RotatedBy(rotAngle, center) * 4f,
					new Rectangle?(rectangle), color, npc.rotation, origin2, npc.scale, spriteEffects, 0f);
			}
			Color color29 = npc.GetAlpha(color25);
			Main.spriteBatch.Draw(texture2D3, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY),
				new Rectangle?(rectangle), color29, npc.rotation, origin2, npc.scale, spriteEffects, 0f);
		}
		#endregion
	}
	public class ForestGuardian_Left : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Guardian's Fist");
			NPCID.Sets.TrailCacheLength[npc.type] = 5;
			NPCID.Sets.TrailingMode[npc.type] = 0;

			Main.npcFrameCount[npc.type] = 3;
		}
		#region AIState
		private enum AIState
		{
			Idle,
			Dash,
			Beam,
			Grab
		}
		private AIState State
		{
			get => (AIState)npc.ai[1];
			set => npc.ai[1] = (int)value;
		}
		#endregion

		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.width = 62;
			npc.height = 96;

			npc.damage = 30;
			npc.defense = 8;
			npc.lifeMax = 350;

			npc.noTileCollide = true;
			npc.noGravity = true;
			npc.lavaImmune = true;
			npc.boss = true;
			npc.netUpdate = true;

			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.DD2_CrystalCartImpact;
			npc.DeathSound = SoundID.Item14;
		}
		private int sec; 
		private int AITimer;
		private int BeamTimer;
		private int DashTimer;
		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			if (State == AIState.Dash || State == AIState.Idle)
			{
				npc.frame.Y = 2 * frameHeight;
			}
			if (State == AIState.Beam)
			{
				npc.frame.Y = 1 * frameHeight;
			}
			if (State == AIState.Grab)
			{
				npc.frame.Y = 0 * frameHeight;
			}
		}
		public override void AI()
		{
			Player target = Main.player[npc.target];
			NPC boss = Main.npc[(int)npc.ai[0]];
			if (target.dead || !target.active)
			{
				npc.TargetClosest(false);
				npc.velocity = new Vector2(0f, -10f);
				npc.timeLeft--;
				if (npc.timeLeft <= 0)
                {
					npc.active = false;
                }
			}
			sec++;
			AITimer++;
			if (sec == 60)
			{
				Main.NewText("Guardian's Fist - Timer Value:" + $"{AITimer}");
				sec = 0;
			}
			if (AITimer == 300)
				State = AIState.Dash;
            
			if (AITimer == 900)
				State = AIState.Beam;
			
			if (AITimer == 1200)
				AITimer = 0;
            
			if (State == AIState.Idle)
            {
				var angle = target.Center - npc.Center;
				npc.rotation = angle.ToRotation() + MathHelper.PiOver2;
				angle.Normalize();
				angle.X *= 3f;
				angle.Y *= 3f;

				var position = boss.Center - new Vector2(200, -30);
				float speed = Vector2.Distance(npc.Center, position);
				speed = MathHelper.Clamp(speed, -18f, 18f);

				Move(position, speed);
			}
			if (State == AIState.Beam) // This is the same as the "Idle" state but it shoots beams.
			{
				BeamTimer++;
				var angle = target.Center - npc.Center;
				npc.rotation = angle.ToRotation() + MathHelper.PiOver2;
				angle.Normalize();
				angle.X *= 3f;
				angle.Y *= 3f;

				var position = boss.Center - new Vector2(200, -30);
				float speed = Vector2.Distance(npc.Center, position);
				speed = MathHelper.Clamp(speed, -18f, 18f);

				Move(position, speed);
				if (BeamTimer >= 120)
				{
					Projectile.NewProjectile(npc.Center, angle * 2.5f, ProjectileID.EyeBeam, 5, 0f, Main.myPlayer);
					BeamTimer = 0;
				}
			}
			if (State == AIState.Dash)
			{
				var angle = target.Center - npc.Center;
				npc.rotation = angle.ToRotation() + MathHelper.PiOver2;
				angle.Normalize();
				angle.X *= 2f;
				angle.Y *= 2f;
				DashTimer++;
				if (DashTimer >= Main.rand.Next(90, 120))
				{
					npc.TargetClosest();
					npc.netUpdate = true;
					Vector2 PlayerPosition = new Vector2(target.Center.X - npc.Center.X, target.Center.Y - npc.Center.Y);
					PlayerPosition.Normalize();
					npc.velocity = PlayerPosition * 8f;
					DashTimer = 0;
				}
			}
		}
		private void Move(Vector2 position, float speed)
		{
			Vector2 direction = npc.DirectionTo(position);

			Vector2 velocity = direction * speed;

			npc.velocity = Vector2.SmoothStep(npc.velocity, velocity, 0.2f);
		}
		public override bool CheckActive()
		{
			if (!NPC.AnyNPCs(NPCType<ForestGuardian>()))
				npc.active = false;

			return base.CheckActive();
		}
	}
	public class ForestGuardian_Right : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Guardian's Fist");
			NPCID.Sets.TrailCacheLength[npc.type] = 5;
			NPCID.Sets.TrailingMode[npc.type] = 0;

			Main.npcFrameCount[npc.type] = 3;
		}
		#region AIState
		private enum AIState
		{
			Idle,
			Dash,
			Beam,
			Grab
		}
		private AIState State
		{
			get => (AIState)npc.ai[1];
			set => npc.ai[1] = (int)value;
		}
		#endregion

		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.width = 62;
			npc.height = 96;

			npc.damage = 30;
			npc.defense = 8;
			npc.lifeMax = 350;

			npc.noTileCollide = true;
			npc.noGravity = true;
			npc.lavaImmune = true;
			npc.boss = true;
			npc.netUpdate = true;

			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.DD2_CrystalCartImpact;
			npc.DeathSound = SoundID.Item14;
		}
		private int AITimer;
		private int BeamTimer;
		private int DashTimer;
		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			if (State == AIState.Dash || State == AIState.Idle)
			{
				npc.frame.Y = 2 * frameHeight;
			}
			if (State == AIState.Beam)
			{
				npc.frame.Y = 1 * frameHeight;
			}
			if (State == AIState.Grab)
			{
				npc.frame.Y = 0 * frameHeight;
			}
		}
		public override void AI()
		{
			Player target = Main.player[npc.target];
			NPC boss = Main.npc[(int)npc.ai[0]];
			if (target.dead || !target.active)
			{
				npc.TargetClosest(false);
				npc.velocity = new Vector2(0f, -10f);
				npc.timeLeft--;
				if (npc.timeLeft <= 0)
				{
					npc.active = false;
				}
			}
			AITimer++;
			if (AITimer == 300)
				State = AIState.Dash;

			if (AITimer == 900)
				State = AIState.Beam;

			if (AITimer == 1200)
				AITimer = 0;

			if (State == AIState.Idle)
			{
				var angle = target.Center - npc.Center;
				npc.rotation = angle.ToRotation() + MathHelper.PiOver2;
				angle.Normalize();
				angle.X *= 3f;
				angle.Y *= 3f;

				var position = boss.Center + new Vector2(200, 30);
				float speed = Vector2.Distance(npc.Center, position);
				speed = MathHelper.Clamp(speed, -18f, 18f);

				Move(position, speed);
			}
			if (State == AIState.Beam) // This is the same as the "Idle" state but it shoots beams.
			{
				BeamTimer++;
				var angle = target.Center - npc.Center;
				npc.rotation = angle.ToRotation() + MathHelper.PiOver2;
				angle.Normalize();
				angle.X *= 3f;
				angle.Y *= 3f;

				var position = boss.Center + new Vector2(200, 30);
				float speed = Vector2.Distance(npc.Center, position);
				speed = MathHelper.Clamp(speed, -18f, 18f);

				Move(position, speed);
				if (BeamTimer >= 120)
				{
					Projectile.NewProjectile(npc.Center, angle * 2.5f, ProjectileID.EyeBeam, 5, 0f, Main.myPlayer);
					BeamTimer = 0;
				}
			}
			if (State == AIState.Dash)
			{
				var angle = target.Center - npc.Center;
				npc.rotation = angle.ToRotation() + MathHelper.PiOver2;
				angle.Normalize();
				angle.X *= 2f;
				angle.Y *= 2f;
				DashTimer++;
				if (DashTimer >= Main.rand.Next(90, 120))
				{
					npc.TargetClosest();
					npc.netUpdate = true;
					Vector2 PlayerPosition = new Vector2(target.Center.X - npc.Center.X, target.Center.Y - npc.Center.Y);
					PlayerPosition.Normalize();
					npc.velocity = PlayerPosition * 8f;
					DashTimer = 0;
				}

			}
		}
		private void Move(Vector2 position, float speed)
		{
			Vector2 direction = npc.DirectionTo(position);

			Vector2 velocity = direction * speed;

			npc.velocity = Vector2.SmoothStep(npc.velocity, velocity, 0.2f);
		}
		public override bool CheckActive()
		{
			if (!NPC.AnyNPCs(NPCType<ForestGuardian>()))
				npc.active = false;

			return base.CheckActive();
		}
	}
}