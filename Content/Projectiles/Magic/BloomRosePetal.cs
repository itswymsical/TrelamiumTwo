using TrelamiumTwo.Content.Dusts;
using TrelamiumTwo.Core;
using TrelamiumTwo.Utilities.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Projectiles.Magic
{
	public class BloomRosePetal : ModProjectile
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Bloom Rose Petal");

		public override void SetDefaults()
		{
			projectile.magic = true;
			projectile.friendly = true; 

			projectile.width = projectile.height = 10;

			projectile.timeLeft = 120;
			projectile.aiStyle = 0;
		}

		public override void AI()
		{
			projectile.ai[0]++;

			if (projectile.ai[0] > 20f)
				projectile.velocity.Y += 0.2f;

			if (Main.rand.NextBool(10))
			{
				Dust.NewDustDirect(projectile.Center, 0, 0, ModContent.DustType<PinkPetal>(), -projectile.velocity.X, -projectile.velocity.Y);

				projectile.netUpdate = true;
			}

			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);

			return true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) => projectile.DrawProjectileCentered(spriteBatch, lightColor);
	}
}
