#region Using Directives
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using TrelamiumTwo.Common.Players;
using static Terraria.ModLoader.ModContent;
#endregion

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
			NPCID.Sets.TrailCacheLength[npc.type] = 5;
			NPCID.Sets.TrailingMode[npc.type] = 0;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.width = 200;
			npc.height = 184;

			npc.damage = 0;
			npc.defense = 8;
			npc.lifeMax = 1600;

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
			spawnTimer++;
			Player target = Main.player[npc.target];
			if (!spawn)
			{
				NPC.NewNPC((int)npc.Center.X - 180, (int)npc.Center.Y + 40, NPCType<ForestGuardian_Left>());
				NPC.NewNPC((int)npc.Center.X + 180, (int)npc.Center.Y + 40, NPCType<ForestGuardian_Right>());
				spawn = true;
			}

			if (State == AIState.Idle)	
				Movement(); Boulders();

			if (State == AIState.Smash)
			{

			}
		}
		private void Boulders()
		{
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
		private void Smash()
		{

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
				string header = "-- Forest Guardian --";
				string subheader = "-- Spellbound Forest Protector --";
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, header, new Vector2((float)(Main.screenWidth / 2) - Main.fontDeathText.MeasureString(header).X / 2f,
					(float)(Main.screenHeight / 10f)), default, 0, new Vector2((Main.screenWidth / 2)), default, -1, 2);
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, subheader, new Vector2((float)(Main.screenWidth / 2) - Main.fontDeathText.MeasureString(header).X / 2f,
					(float)(Main.screenHeight / 10f)), default, 0, new Vector2((Main.screenWidth / 2)), default, -1, 2);
			}
			int alpha = (int)npc.ai[1] / 4;
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (npc.spriteDirection == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Color color25 = Lighting.GetColor((int)(npc.position.X + npc.width * 0.5) / 16, (int)((npc.position.Y + npc.height * 0.5) / 16.0));
			Texture2D texture2D3 = Main.npcTexture[npc.type];
			int num154 = Main.npcTexture[npc.type].Height;
			int y3 = num154;
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

			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.DD2_CrystalCartImpact;
			npc.DeathSound = SoundID.Item14;
		}
		private int DashTimer;
		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			if (++npc.frameCounter > 3)
			{
				npc.frameCounter = 0;
				npc.frame.Y = (npc.frame.Y + frameHeight) % (frameHeight * Main.npcFrameCount[npc.type]);
			}
		}
		public override void AI()
		{
			Player target = Main.player[npc.target];
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

			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.DD2_CrystalCartImpact;
			npc.DeathSound = SoundID.Item14;
		}
		private int DashTimer;
        public override void FindFrame(int frameHeight)
        {
			npc.spriteDirection = npc.direction;
			if (++npc.frameCounter > 3)
			{
				npc.frameCounter = 0;
				npc.frame.Y = (npc.frame.Y + frameHeight) % (frameHeight * Main.npcFrameCount[npc.type]);
			}
		}
        public override void AI()
		{
			Player target = Main.player[npc.target];
			var angle = target.Center - npc.Center;
			npc.rotation = angle.ToRotation() + MathHelper.PiOver2;
			angle.Normalize();
			angle.X *= 2f;
			angle.Y *= 2f;
			DashTimer++;
			if (DashTimer >= Main.rand.Next(90, 120))
			{
				npc.TargetClosest(true);
				npc.netUpdate = true;
				Vector2 PlayerPosition = new Vector2(target.Center.X - npc.Center.X, target.Center.Y - npc.Center.Y);
				PlayerPosition.Normalize();
				npc.velocity = PlayerPosition * 8f;
				DashTimer = 0;
			}
		}
		public override bool CheckActive()
		{
			if (!NPC.AnyNPCs(NPCType<ForestGuardian>()))
				npc.active = false;      

            return base.CheckActive();
        }
    }
}