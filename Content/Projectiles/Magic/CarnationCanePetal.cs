using System;

using Terraria;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TrelamiumTwo.Helpers;

namespace TrelamiumTwo.Content.Projectiles.Magic
{
	public class CarnationCanePetal : ModProjectile
	{
		private readonly float
			maxXVelocity = 3.5f,
			maxYVelocity = 2;

		private readonly float
			xAcceleration = 0.08f,
			yAcceleration = 0.1f;
		public override string Texture => Assets.Projectiles.Magic + "CarnationCanePetal";
		public override void SetStaticDefaults() => DisplayName.SetDefault("Petal");
		
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 12;

			Projectile.friendly = true;
			// projectile.ignoreWater = false;

			Projectile.manualDirectionChange = true;
		}

		public override bool PreAI()
		{
			if (Projectile.localAI[0] == 0)
			{
				Projectile.localAI[0] = 1;
				Projectile.direction = Math.Sign(Projectile.velocity.X);
			}

			if (++Projectile.ai[0] >= 5)
			{
				float xVelocityModifier = (float)Math.Cos(Projectile.ai[0] * 0.05f);
				float yVelocityModifier = -(float)Math.Sin(Projectile.ai[0] * 0.1f);

				Vector2 targetVelocity = new Vector2(xVelocityModifier, yVelocityModifier) * new Vector2(maxXVelocity, maxYVelocity);

				if (targetVelocity.Y < 0)
				{
					targetVelocity.Y *= 0.35f;
				}

				if (Math.Sign(Projectile.velocity.X) != Math.Sign(targetVelocity.X))
				{
					Projectile.velocity.X *= 0.98f;
				}
				if (Math.Sign(Projectile.velocity.Y) != Math.Sign(targetVelocity.Y))
				{
					Projectile.velocity.Y *= 0.98f;
				}

				// Apply proper velocity depending on the current targetVelocity.
				Projectile.velocity.X += xAcceleration * Math.Sign(targetVelocity.X - Projectile.velocity.X);
				Projectile.velocity.Y += yAcceleration * Math.Sign(targetVelocity.Y - Projectile.velocity.Y);

				// Rotate 'slowly' towards a falling angle.
				Projectile.rotation = MathHelper.Lerp(Projectile.rotation,
					Projectile.velocity.X * -0.2f + MathHelper.PiOver2 * Projectile.direction,
					0.1f);
			}
			
			// If the projectile is still shooting forward, aim it towards where it's heading.
			if (Projectile.velocity.Length() >= 4)
			{
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
			}

			return (false);
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Projectile.velocity.X != oldVelocity.X)
			{
				Projectile.velocity.X = -oldVelocity.X;
				return (false);
			}
			if (Main.myPlayer == Projectile.owner)
			{
				int posY = (int)((Projectile.position.Y + Projectile.height) / 16) * 16;

				Vector2 newProjectilePosition = new Vector2(Projectile.position.X, posY);

				Projectile.NewProjectile(newProjectilePosition, Vector2.Zero, ModContent.ProjectileType<CarnationCaneBrush>(), (int)(Projectile.damage * 0.66f), 0f, Projectile.owner);
			}

			return (true);
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 10; ++i)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.PinkPetal>(), 0, 0, 100);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
			=> Projectile.DrawProjectileCentered(spriteBatch, lightColor);
	}
}
