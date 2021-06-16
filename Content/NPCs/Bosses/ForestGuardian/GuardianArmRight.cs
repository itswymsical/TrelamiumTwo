using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using static Terraria.ModLoader.ModContent;

namespace TrelamiumTwo.Content.NPCs.Bosses.ForestGuardian
{
	[AutoloadBossHead]
	public class GuardianArmRight : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Guardian's Fist");
			NPCID.Sets.TrailCacheLength[npc.type] = 5;
			NPCID.Sets.TrailingMode[npc.type] = 0;

			Main.npcFrameCount[npc.type] = 3;
		}
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
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.width = 62;
			npc.height = 96;

			npc.damage = 30;
			npc.defense = 8;
			npc.lifeMax = 800;

			npc.noTileCollide = true;
			npc.noGravity = true;
			npc.lavaImmune = true;
			npc.netUpdate = true;

			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.DD2_CrystalCartImpact;
			npc.DeathSound = SoundID.Item14;
		}
		private int AITimer;
		private int BeamTimer;
		private int DashTimer; 
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax / Main.expertLife * 1.35f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.55f);
		}
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
				State = AIState.Beam;

			if (AITimer == 900)
				State = AIState.Dash;

			if (AITimer == 1200)
				AITimer = 0;

			if (State == AIState.Idle)
			{
				var angle = target.Center - npc.Center;
				npc.rotation = angle.ToRotation() + MathHelper.PiOver2;
				angle.Normalize();
				angle.X *= 3f;
				angle.Y *= 3f;

				var position = boss.Center + new Vector2(200, 80);
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

				var position = boss.Center + new Vector2(200, 80);
				float speed = Vector2.Distance(npc.Center, position);
				speed = MathHelper.Clamp(speed, -18f, 18f);

				Move(position, speed);
				if (BeamTimer >= 120)
				{
					Projectile.NewProjectile(npc.Center, angle * 2.5f, ProjectileID.EyeBeam, 5, 0f, Main.myPlayer);
					BeamTimer = 0;
				}
				if (npc.life < npc.lifeMax * 0.20f)
				{
					if (BeamTimer >= 30)
					{
						Projectile.NewProjectile(npc.Center, angle * 2.5f, ProjectileID.EyeBeam, 6, 0f, Main.myPlayer);
						BeamTimer = 0;
					}
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
				if (npc.life < npc.lifeMax * 0.20f)
				{
					if (DashTimer >= 60)
					{
						npc.TargetClosest();
						npc.netUpdate = true;
						Vector2 PlayerPosition = new Vector2(target.Center.X - npc.Center.X, target.Center.Y - npc.Center.Y);
						PlayerPosition.Normalize();
						npc.velocity = PlayerPosition * 10.5f;
						DashTimer = 0;
					}
				}
			}
		}
		public override void BossHeadRotation(ref float rotation) => rotation = npc.rotation;
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
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				for (int num2 = 0; num2 < 10; num2++)
				{
					int num = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.Dirt, 0f, 0f, 125, default, 0.65f);
					Main.dust[num].velocity *= 3f;
				}
			}
		}
	}
}
