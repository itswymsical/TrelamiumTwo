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
			projectile.width = projectile.height = 8;
			
			projectile.scale = 1.25f;
			projectile.penetrate = 1;
			projectile.timeLeft = 180;
			
			projectile.ranged = 
				projectile.friendly = 
				projectile.ignoreWater = 
				projectile.tileCollide = true;
		}

		public override bool PreAI()
		{
			if (++projectile.ai[0] >= 10)
			{
				projectile.velocity.Y += 0.2f;
				projectile.velocity.X *= 0.98f;
			}

			projectile.rotation += projectile.velocity.Length() * 0.1f * projectile.direction;

			return false;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 78, 0f, 0f, 100, default, 2f);
				dust.scale *= 0.756f;
				dust.noGravity = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) => projectile.DrawProjectileCentered(spriteBatch, lightColor);
	}
}
