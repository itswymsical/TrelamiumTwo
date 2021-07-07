using TrelamiumTwo.Core;
using TrelamiumTwo.Helpers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Projectiles.Summon
{
	public class Tumbleweed : ModProjectile
	{
		public override string Texture => Assets.Projectiles.Summon + "Tumbleweed";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tumbleweed");

			Main.projPet[projectile.type] = true;

			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.tileCollide = true;
			projectile.friendly = true;
			projectile.minion = true;

			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 20;

			projectile.width = projectile.height = 44;
			projectile.scale = 0.8f;
			projectile.minionSlots = 1f;
			projectile.penetrate = -1;

			projectile.aiStyle = -1;
			aiType = -1;
		}

		public enum States
		{
			Idle,
			Attack
		}

		public States State 
		{
			get => (States)projectile.ai[0];
			set => projectile.ai[0] = (int)value;
		}

		private NPC Target => Main.npc[(int)projectile.ai[1]];

		private const float maxScale = 1.25f;

		public override void AI()
		{
			Player player = Main.player[projectile.owner];

			if (player.dead || !player.active)
				player.ClearBuff(ModContent.BuffType<Buffs.Minions.Tumbleweed>());

			if (player.HasBuff(ModContent.BuffType<Buffs.Minions.Tumbleweed>()))
				projectile.timeLeft = 2;

			float attackRange = 40f * 16f;

			if (State == States.Idle)
			{
				Vector2 idlePosition = player.Center - new Vector2((36f + projectile.minionPos * 36f) * player.direction, 0f);

				Move(idlePosition);

				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];

					if (npc.CanBeChasedBy() && npc.Distance(projectile.Center) < attackRange)
					{
						State = States.Attack;
						projectile.ai[1] = npc.whoAmI;
					}
				}
			}

			if (State == States.Attack)
			{
				Move(Target.Center);

				if (!Target.active || Target.Distance(projectile.Center) > attackRange)
					State = States.Idle;
			}

			projectile.velocity.Y += 0.3f;

			projectile.scale += Math.Abs(projectile.velocity.X) * 0.0007f;
			projectile.scale = MathHelper.Clamp(projectile.scale, 0.8f, maxScale);

			projectile.damage += (int)(Math.Abs(projectile.velocity.X) * 0.0007f);
			projectile.damage = (int)MathHelper.Clamp(projectile.damage, 9, 21);

			projectile.rotation += Math.Abs(projectile.velocity.X) / 20f * projectile.direction;

			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];

				if (npc.CanBeChasedBy() && npc.Distance(projectile.Center) < attackRange)
					projectile.ai[1] = npc.whoAmI;
			}

			float maxPlayerRange = 80f * 16f;

			if (player.Distance(projectile.Center) > maxPlayerRange)
			{
				projectile.position = player.Center;

				projectile.netUpdate = true;
			}

			Collision.StepUp(ref projectile.position, ref projectile.velocity, projectile.width, projectile.height, ref projectile.stepSpeed, ref projectile.gfxOffY);
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Player player = Main.player[projectile.owner];

			if (player.position.Y < projectile.position.Y - 100f && Main.rand.NextBool(4))
			{
				projectile.velocity.Y = Main.rand.NextFloat(-10f, -8f) - projectile.scale + 1f;

				projectile.netUpdate = true;
			}

			return false;
		}

		public override bool? CanHitNPC(NPC target)
		{
			if (State == States.Attack)
				return target.whoAmI == Target.whoAmI;

			return true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			projectile.velocity.Y = Main.rand.NextFloat(-8f, -4f);

			if (Main.rand.NextBool(2))
				projectile.velocity.X = Main.rand.NextFloat(-4f, 4f);

			projectile.scale -= projectile.scale / 4f;
			projectile.damage -= projectile.damage / 4;
			projectile.netUpdate = true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			projectile.DrawProjectileTrailCentered(spriteBatch, lightColor, 0.8f, 0.5f, 1);
			return true;
		}

		public override bool MinionContactDamage() => true;

		public override bool? CanCutTiles() => false;

		private void Move(Vector2 position)
		{
			var direction = Vector2.Normalize(position - projectile.Center);
			direction *= 8f - projectile.scale + 1f;

			float inertia = 8f;
			Vector2 velocity = (projectile.velocity * (inertia - 1f) + direction) / inertia;

			projectile.velocity.X = velocity.X;		
		}
	}
}
