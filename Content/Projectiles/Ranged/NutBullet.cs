using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TrelamiumTwo.Helpers;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Projectiles.Ranged
{
	public sealed class NutBullet : ModProjectile
	{
		public override string Texture => Assets.Projectiles.Ranged + "NutBullet";
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 2;    
		}
		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 12;
			
			projectile.aiStyle = 1;
			projectile.penetrate = 1;
			projectile.timeLeft = 120;
			
			projectile.ranged = true;
			projectile.friendly = true;
			projectile.tileCollide = true;
		}

		public override void AI() => projectile.velocity.X *= 1.03f;

		public override void Kill(int timeLeft)
		{
			Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 78, 0f, 0f, 100, default, 2f);
			dust.scale *= 0.756f;
			dust.noGravity = true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			projectile.DrawProjectileTrailCentered(spriteBatch, lightColor);

			return projectile.DrawProjectileCentered(spriteBatch, lightColor);
		}
	}
}
