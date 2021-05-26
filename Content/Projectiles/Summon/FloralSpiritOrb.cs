using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TrelamiumTwo.Common.Extensions;

namespace TrelamiumTwo.Content.Projectiles.Summon
{
	internal class FloralSpiritOrb : ModProjectile
	{
		private readonly float maxSpeed = 10f;

		private readonly float chargeTime = 60;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Floral Orb");
			
			ProjectileID.Sets.MinionShot[projectile.type] = true;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
		}
		public override void SetDefaults()
		{
			projectile.width = projectile.height = 22;

			projectile.alpha = 255;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;

			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
		}

		public override bool PreAI()
		{
			NPC target = Main.npc[(int)projectile.ai[1]];
			Projectile owner = Main.projectile[(int)projectile.ai[0]];
			
			projectile.rotation += 0.1f;

			if (!target.active)
			{
				projectile.Kill();
				return (false);
			}

			if (++projectile.localAI[0] <= chargeTime)
			{
				projectile.Center = owner.Center - Vector2.UnitY * 50;

				// Visual charging effects.
				SpawnChargeDust();
				projectile.Opacity = projectile.localAI[0] / chargeTime;

				if (projectile.localAI[0] == chargeTime)
				{
					projectile.netUpdate = true;
					projectile.velocity = Vector2.Normalize(target.position - projectile.Center) * maxSpeed;
				}
			}
			else
			{
				if (target.active)
				{
					float desiredAngle = (target.position - projectile.Center).ToRotation();

					projectile.velocity = projectile.velocity.ToRotation().AngleLerp(desiredAngle, 0.1f).ToRotationVector2() * maxSpeed;
				}

				if (projectile.timeLeft < 3)
				{
					projectile.Opacity = 0f;

					projectile.position = projectile.Center;
					projectile.width = projectile.height = 80;
					projectile.Center = projectile.position;
				}
			}

			// Visual.
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 2,
					projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 100);
			}

			Lighting.AddLight(projectile.Center, new Vector3(.1f, .5f, .2f));

			return (false);
		}

		public override bool CanDamage() => projectile.localAI[0] > chargeTime;

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.DryadsWardDebuff, 180);

			if (projectile.timeLeft > 3)
			{
				projectile.timeLeft = 3;
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 15; ++i)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 2, 0, 0, 100);
				dust.velocity *= 2f;
			}

			for (int i = 0; i < 10; ++i)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 78, 0, 0, 100);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			this.DrawProjectileTrailCentered(spriteBatch, lightColor * projectile.Opacity);

			Texture2D glow = ModContent.GetTexture(this.Texture + "_Glow");
			Vector2 origin = glow.Size() / 2;
			spriteBatch.Draw(glow, projectile.Center - Main.screenPosition, null, Color.White * (projectile.Opacity - 0.2f), 0, origin, projectile.scale + 0.2f, SpriteEffects.None, 0f);

			return this.DrawProjectileCentered(spriteBatch, lightColor * projectile.Opacity);
		}

		private void SpawnChargeDust()
		{
			Vector2 dustPosition = projectile.Center + Vector2.UnitY.RotatedByRandom(MathHelper.TwoPi) * 30;

			Vector2 dustVelocity = Vector2.Normalize(projectile.Center - dustPosition);

			Dust.NewDustPerfect(dustPosition, 2, dustVelocity, 255 - projectile.alpha);
		}

		#region Networking

		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(projectile.localAI[0]);
		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{
			projectile.localAI[0] = reader.ReadSingle();
		}

		#endregion
	}
}
