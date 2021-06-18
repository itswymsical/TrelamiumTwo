using TrelamiumTwo.Content.Dusts;
using TrelamiumTwo.Core;
using TrelamiumTwo.Helpers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Projectiles.Magic
{
	public class BloomRose : ModProjectile
	{
		public override string Texture => Assets.Items.Materials + "BloomRose";

		public override void SetDefaults()
		{
			projectile.magic = true;
			projectile.friendly = true;

			projectile.width = projectile.height = 20;

			projectile.timeLeft = 180;
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

			projectile.rotation += projectile.velocity.X / 25f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Main.PlaySound(SoundID.Dig, projectile.position);
			Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);

			return true;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 16; i++)
			{
				var velocity = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f));
				Dust.NewDustDirect(projectile.Center, 0, 0, ModContent.DustType<PinkPetal>(), velocity.X, velocity.Y);
			}

			int petalAmount = Main.rand.Next(2, 6);
			for (int i = 0; i < petalAmount; i++)
			{
				float rotation = i / (float)petalAmount * MathHelper.TwoPi;
				var velocity = rotation.ToRotationVector2() * 5f;

				Projectile.NewProjectile(projectile.Center, velocity, ModContent.ProjectileType<BloomRosePetal>(), projectile.damage / 2, projectile.knockBack / 2f, projectile.owner);
				
				projectile.netUpdate = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) => projectile.DrawProjectileCentered(spriteBatch, lightColor);
	}
}
