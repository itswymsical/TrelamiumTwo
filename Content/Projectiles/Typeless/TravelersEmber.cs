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
			get => (AIState)Projectile.ai[0];
			set => Projectile.ai[0] = (int)value;
		}

		private Vector2 TargetPosition
		{
			get => new Vector2(Projectile.localAI[0], Projectile.localAI[1]);
			set
			{
				Projectile.localAI[0] = value.X;
				Projectile.localAI[1] = value.Y;
			}
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Traveler's Ember");
		}
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 8;

			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			// projectile.tileCollide = false;
		}

		public override bool PreAI()
		{
			Player owner = Main.player[Projectile.owner];

			if (!CheckAliveState(owner))
			{
				Projectile.Kill();
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

			Projectile.timeLeft = 60;

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

				if (++Projectile.ai[1] >= 1600)
				{
					Projectile.netUpdate = true;
					Projectile.ai[0] = Projectile.ai[1] = 0f;
				}
			}

			// Prevent swarming of projectile.
			for (int i = 0; i < Main.maxProjectiles; ++i)
			{
				if (Main.projectile[i].active && Main.projectile[i].type == Projectile.type && Main.projectile[i].owner == Projectile.owner)
				{
					Vector2 directionFromOther = Projectile.Center - Main.projectile[i].Center;
					if (directionFromOther.LengthSquared() <= 256)
					{
						directionFromOther.SafeNormalize(Vector2.UnitY);
						Projectile.velocity += directionFromOther * 0.03f;
					}
				}
			}

			// Slightly randomized floating code.
			float distanceToTargetPosition = Vector2.Distance(Projectile.Center, targetPosition);
			if (distanceToTargetPosition >= 32)
			{
				if (distanceToTargetPosition >= 500)
				{
					Projectile.netUpdate = true;
					Projectile.position = targetPosition;
				}
				else
				{
					currentspeed = MathHelper.Lerp(minSpeed, maxSpeed, distanceToTargetPosition / 500);
					Projectile.velocity += Projectile.DirectionTo(targetPosition) * (currentspeed / 10f);
				}
			}

			Projectile.velocity = Vector2.Clamp(Projectile.velocity, -Vector2.One * currentspeed, Vector2.One * currentspeed);
		}

		private void Effects()
		{
			if (Main.rand.NextBool(2))
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Fire, Projectile.velocity.X * -0.2f, Projectile.velocity.Y * -0.2f, 100);
				dust.noGravity = true;
			}

			float lightModifier = 0.5f;
			if (State == AIState.Holding)
			{
				lightModifier = 1f;
			}
			
			Lighting.AddLight(Projectile.Center, new Vector3(0.5f, 0.2f, 0f) * lightModifier);
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
