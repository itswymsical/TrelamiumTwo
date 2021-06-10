using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TrelamiumTwo.Common.Extensions;

namespace TrelamiumTwo.Content.Projectiles.Melee
{
	public class DustiliteClaymoreProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dustilite Claymore");

			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			projectile.width = projectile.height = 16;

			projectile.alpha = 255;
			projectile.penetrate = -1;

			projectile.melee = projectile.friendly = true;
			
			projectile.tileCollide = false;
		}

		public override bool PreAI()
		{
			projectile.ai[0] -= projectile.velocity.Length();
			if (projectile.ai[0] > 0)
			{
				projectile.alpha -= 15;
			}
			else
			{
				projectile.alpha += 10;
				if (projectile.alpha >= 255)
				{
					projectile.Kill();
				}
			}

			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver4;

			Lighting.AddLight(projectile.Center, new Vector3(0.5f, 0.5f, 0f) * projectile.Opacity);

			return (false);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			this.DrawProjectileTrailCentered(spriteBatch, lightColor, projectile.Opacity - 0.4f, 0.15f);

			return this.DrawProjectileCentered(spriteBatch, lightColor * projectile.Opacity);
		}
	}
}
