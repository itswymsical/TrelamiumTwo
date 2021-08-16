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
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2;    
		}
		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 12;
			
			Projectile.aiStyle = 1;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 120;
			
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.friendly = true;
			Projectile.tileCollide = true;
		}

		public override void AI() => Projectile.velocity.X *= 1.03f;

		public override void Kill(int timeLeft)
		{
			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 78, 0f, 0f, 100, default, 2f);
			dust.scale *= 0.756f;
			dust.noGravity = true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Projectile.DrawProjectileTrailCentered(spriteBatch, lightColor);

			return Projectile.DrawProjectileCentered(spriteBatch, lightColor);
		}
	}
}
