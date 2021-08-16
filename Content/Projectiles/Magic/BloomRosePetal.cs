using TrelamiumTwo.Content.Dusts;
using TrelamiumTwo.Core;
using TrelamiumTwo.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TrelamiumTwo.Core.Mechanics.Trails;

namespace TrelamiumTwo.Content.Projectiles.Magic
{
	public class BloomRosePetal : ModProjectile
	{
		public override string Texture => Assets.Items.Materials + "BloomRosePetal";

		public override void SetStaticDefaults() => DisplayName.SetDefault("Bloom Rose Petal");

		public override void SetDefaults()
		{
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true; 

			Projectile.width = Projectile.height = 10;

			Projectile.timeLeft = 120;
			Projectile.aiStyle = 0;
		}

		public override void AI()
		{
			Projectile.ai[0]++;

			if (Projectile.ai[0] > 20f)
				Projectile.velocity.Y += 0.2f;

			if (Main.rand.NextBool(10))
			{
				Dust.NewDustDirect(Projectile.Center, 0, 0, ModContent.DustType<PinkPetal>(), -Projectile.velocity.X, -Projectile.velocity.Y);

				Projectile.netUpdate = true;
			}

			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
		}
        public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
			return true;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) => Projectile.DrawProjectileCentered(spriteBatch, lightColor);
	}
}
