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

			Main.projPet[Projectile.type] = true;

			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

			ProjectileID.Sets.Homing[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.tileCollide = true;
			Projectile.friendly = true;
			Projectile.minion = true;

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 20;

			Projectile.width = Projectile.height = 44;
			Projectile.scale = 0.8f;
			Projectile.minionSlots = 1f;
			Projectile.penetrate = -1;

			Projectile.aiStyle = -1;
			aiType = -1;
		}

		public enum States
		{
			Idle,
			Attack
		}

		public States State 
		{
			get => (States)Projectile.ai[0];
			set => Projectile.ai[0] = (int)value;
		}

		private NPC Target => Main.npc[(int)Projectile.ai[1]];

		private const float maxScale = 1.25f;

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			if (player.dead || !player.active)
				player.ClearBuff(ModContent.BuffType<Buffs.Minions.Tumbleweed>());

			if (player.HasBuff(ModContent.BuffType<Buffs.Minions.Tumbleweed>()))
				Projectile.timeLeft = 2;

			float attackRange = 40f * 16f;

			if (State == States.Idle)
			{
				Vector2 idlePosition = player.Center - new Vector2((36f + Projectile.minionPos * 36f) * player.direction, 0f);

				Move(idlePosition);

				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];

					if (npc.CanBeChasedBy() && npc.Distance(Projectile.Center) < attackRange)
					{
						State = States.Attack;
						Projectile.ai[1] = npc.whoAmI;
					}
				}
			}

			if (State == States.Attack)
			{
				Move(Target.Center);

				if (!Target.active || Target.Distance(Projectile.Center) > attackRange)
					State = States.Idle;
			}

			Projectile.velocity.Y += 0.3f;

			Projectile.scale += Math.Abs(Projectile.velocity.X) * 0.0007f;
			Projectile.scale = MathHelper.Clamp(Projectile.scale, 0.8f, maxScale);

			Projectile.damage += (int)(Math.Abs(Projectile.velocity.X) * 0.0007f);
			Projectile.damage = (int)MathHelper.Clamp(Projectile.damage, 9, 21);

			Projectile.rotation += Math.Abs(Projectile.velocity.X) / 20f * Projectile.direction;

			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];

				if (npc.CanBeChasedBy() && npc.Distance(Projectile.Center) < attackRange)
					Projectile.ai[1] = npc.whoAmI;
			}

			float maxPlayerRange = 80f * 16f;

			if (player.Distance(Projectile.Center) > maxPlayerRange)
			{
				Projectile.position = player.Center;

				Projectile.netUpdate = true;
			}

			Collision.StepUp(ref Projectile.position, ref Projectile.velocity, Projectile.width, Projectile.height, ref Projectile.stepSpeed, ref Projectile.gfxOffY);
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Player player = Main.player[Projectile.owner];

			if (player.position.Y < Projectile.position.Y - 100f && Main.rand.NextBool(4))
			{
				Projectile.velocity.Y = Main.rand.NextFloat(-10f, -8f) - Projectile.scale + 1f;

				Projectile.netUpdate = true;
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
			Projectile.velocity.Y = Main.rand.NextFloat(-8f, -4f);

			if (Main.rand.NextBool(2))
				Projectile.velocity.X = Main.rand.NextFloat(-4f, 4f);

			Projectile.scale -= Projectile.scale / 4f;
			Projectile.damage -= Projectile.damage / 4;
			Projectile.netUpdate = true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Projectile.DrawProjectileTrailCentered(spriteBatch, lightColor, 0.8f, 0.5f, 1);
			return true;
		}

		public override bool MinionContactDamage() => true;

		public override bool? CanCutTiles() => false;

		private void Move(Vector2 position)
		{
			var direction = Vector2.Normalize(position - Projectile.Center);
			direction *= 8f - Projectile.scale + 1f;

			float inertia = 8f;
			Vector2 velocity = (Projectile.velocity * (inertia - 1f) + direction) / inertia;

			Projectile.velocity.X = velocity.X;		
		}
	}
}
