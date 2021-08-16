using Terraria;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TrelamiumTwo.Helpers;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Projectiles.Ranged
{
	public class NutProjectile : ModProjectile
	{
		public override string Texture => Assets.Projectiles.Ranged + "NutProjectile";
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 8;
			
			Projectile.scale = 1.25f;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 180;

			Projectile.DamageType = DamageClass.Ranged;
				Projectile.friendly = 
				Projectile.ignoreWater =
				Projectile.tileCollide = true;
		}

		public override bool PreAI()
		{
			if (++Projectile.ai[0] >= 10)
			{
				Projectile.velocity.Y += 0.2f;
				Projectile.velocity.X *= 0.98f;
			}

			Projectile.rotation += Projectile.velocity.Length() * 0.1f * Projectile.direction;

			return false;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 78, 0f, 0f, 100, default, 2f);
				dust.scale *= 0.756f;
				dust.noGravity = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) => Projectile.DrawProjectileCentered(spriteBatch, lightColor);
	}
}
