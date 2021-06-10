using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Content.Projectiles
{
	public sealed class TravelersEmber : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_" + ProjectileID.None;

		private enum AIState
		{
			Follow = 0,
			Holding = 1
		}
		private AIState State
		{
			get => (AIState)projectile.ai[0];
			set => projectile.ai[0] = (int)value;
		}

		private Vector2 TargetPosition
		{
			get => new Vector2(projectile.localAI[0], projectile.localAI[1]);
			set
			{
				projectile.localAI[0] = value.X;
				projectile.localAI[1] = value.Y;
			}
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Traveler's Ember");
		}
		public override void SetDefaults()
		{
			projectile.width = projectile.height = 8;

			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
		}

		public override bool PreAI()
		{
			Player owner = Main.player[projectile.owner];

			if (!CheckAliveState(owner))
			{
				projectile.Kill();
				return (false);
			}

			Move(owner);

			Effects();

			return (false);
		}

		private bool CheckAliveState(Player owner)
		{
			if (!owner.active || owner.dead)
			{
				return (false);
			}

			if (State != AIState.Holding && owner.inventory[owner.selectedItem].type != ModContent.ItemType<Items.Tools.TravelersLantern>())
			{
				return (false);
			}

			projectile.timeLeft = 60;

			return (true);
		}

		private void Move(Player owner)
		{
			float minSpeed = 1f;
			float maxSpeed = 5f;

			float currentspeed = minSpeed;
			Vector2 targetPosition = owner.Center;

			if (State == AIState.Follow)
			{
				targetPosition += new Vector2(owner.direction * 40f, 12);
			}
			else
			{
				targetPosition = TargetPosition;

				if (++projectile.ai[1] >= 1600)
				{
					projectile.netUpdate = true;
					projectile.ai[0] = projectile.ai[1] = 0f;
				}
			}

			// Prevent swarming of projectile.
			for (int i = 0; i < Main.maxProjectiles; ++i)
			{
				if (Main.projectile[i].active && Main.projectile[i].type == projectile.type && Main.projectile[i].owner == projectile.owner)
				{
					Vector2 directionFromOther = projectile.Center - Main.projectile[i].Center;
					if (directionFromOther.LengthSquared() <= 256)
					{
						directionFromOther.SafeNormalize(Vector2.UnitY);
						projectile.velocity += directionFromOther * 0.03f;
					}
				}
			}

			// Slightly randomized floating code.
			float distanceToTargetPosition = Vector2.Distance(projectile.Center, targetPosition);
			if (distanceToTargetPosition >= 32)
			{
				if (distanceToTargetPosition >= 500)
				{
					projectile.netUpdate = true;
					projectile.position = targetPosition;
				}
				else
				{
					currentspeed = MathHelper.Lerp(minSpeed, maxSpeed, distanceToTargetPosition / 500);
					projectile.velocity += projectile.DirectionTo(targetPosition) * (currentspeed / 10f);
				}
			}

			projectile.velocity = Vector2.Clamp(projectile.velocity, -Vector2.One * currentspeed, Vector2.One * currentspeed);
		}

		private void Effects()
		{
			if (Main.rand.NextBool(2))
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Fire, projectile.velocity.X * -0.2f, projectile.velocity.Y * -0.2f, 100);
				dust.noGravity = true;
			}

			float lightModifier = 0.5f;
			if (State == AIState.Holding)
			{
				lightModifier = 1f;
			}
			
			Lighting.AddLight(projectile.Center, new Vector3(0.5f, 0.2f, 0f) * lightModifier);
		}

		#region Networking

		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.WriteVector2(TargetPosition);
		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{
			TargetPosition = reader.ReadVector2();
		}

		#endregion
	}
}
