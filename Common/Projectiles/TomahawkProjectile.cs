using TrelamiumTwo.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;

namespace TrelamiumTwo.Common.Projectiles
{
	public abstract class TomahawkProjectile : StickyProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.friendly = true;
			projectile.tileCollide = true;

			stickToTile = true;
			stickToNPC = true;

			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 60;

			projectile.timeLeft = 180;
			projectile.penetrate = -1;
			projectile.aiStyle = 0;
		}

		public override void AI()
		{
			if (!stickingToNPC)
			{
				projectile.ai[0]++;

				if (projectile.ai[0] >= 20f)
					projectile.velocity.Y += 0.2f;

				projectile.rotation += (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) / 25f * projectile.direction;
			}

			if (projectile.timeLeft < 255 / 5)
				projectile.alpha += 5;

			base.AI();
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (!stickingToTile)
			{
				Main.PlaySound(SoundID.Dig, projectile.position);
				Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
			}

			return base.OnTileCollide(oldVelocity);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			projectile.DrawProjectileTrailCentered(spriteBatch, drawColor);

			return projectile.DrawProjectileCentered(spriteBatch, drawColor);
		}
	}
}
