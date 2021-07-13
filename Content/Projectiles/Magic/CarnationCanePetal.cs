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

		public override void SetStaticDefaults() => DisplayName.SetDefault("Petal");
		
		public override void SetDefaults()
		{
			projectile.width = projectile.height = 12;

			projectile.friendly = true;
			projectile.ignoreWater = false;

			projectile.manualDirectionChange = true;
		}

		public override bool PreAI()
		{
			if (projectile.localAI[0] == 0)
			{
				projectile.localAI[0] = 1;
				projectile.direction = Math.Sign(projectile.velocity.X);
			}

			if (++projectile.ai[0] >= 5)
			{
				float xVelocityModifier = (float)Math.Cos(projectile.ai[0] * 0.05f);
				float yVelocityModifier = -(float)Math.Sin(projectile.ai[0] * 0.1f);

				Vector2 targetVelocity = new Vector2(xVelocityModifier, yVelocityModifier) * new Vector2(maxXVelocity, maxYVelocity);

				if (targetVelocity.Y < 0)
				{
					targetVelocity.Y *= 0.35f;
				}

				if (Math.Sign(projectile.velocity.X) != Math.Sign(targetVelocity.X))
				{
					projectile.velocity.X *= 0.98f;
				}
				if (Math.Sign(projectile.velocity.Y) != Math.Sign(targetVelocity.Y))
				{
					projectile.velocity.Y *= 0.98f;
				}

				// Apply proper velocity depending on the current targetVelocity.
				projectile.velocity.X += xAcceleration * Math.Sign(targetVelocity.X - projectile.velocity.X);
				projectile.velocity.Y += yAcceleration * Math.Sign(targetVelocity.Y - projectile.velocity.Y);

				// Rotate 'slowly' towards a falling angle.
				projectile.rotation = MathHelper.Lerp(projectile.rotation,
					projectile.velocity.X * -0.2f + MathHelper.PiOver2 * projectile.direction,
					0.1f);
			}
			
			// If the projectile is still shooting forward, aim it towards where it's heading.
			if (projectile.velocity.Length() >= 4)
			{
				projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
			}

			return (false);
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (projectile.velocity.X != oldVelocity.X)
			{
				projectile.velocity.X = -oldVelocity.X;
				return (false);
			}
			if (Main.myPlayer == projectile.owner)
			{
				int posY = (int)((projectile.position.Y + projectile.height) / 16) * 16;

				Vector2 newProjectilePosition = new Vector2(projectile.position.X, posY);

				Projectile.NewProjectile(newProjectilePosition, Vector2.Zero, ModContent.ProjectileType<CarnationCaneBrush>(), (int)(projectile.damage * 0.66f), 0f, projectile.owner);
			}

			return (true);
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 10; ++i)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.PinkPetal>(), 0, 0, 100);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
			=> projectile.DrawProjectileCentered(spriteBatch, lightColor);
	}
}
