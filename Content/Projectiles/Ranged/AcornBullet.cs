using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrelamiumTwo.Content.Projectiles.Ranged
{
	public class AcornBullet : ModProjectile
	{
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

		public override void AI()
			=> projectile.velocity.X *= 0.99f;

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item61, projectile.position);

			for (int j = 0; j < 32; j++)
			{
				float random = Main.rand.NextFloat(-6f, 6f);
				Dust.NewDust(projectile.Center, 0, 0, ModContent.DustType<Dusts.Wood>(), random, random, 0, default, 0.8f);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.projectileTexture[projectile.type];
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, projectile.height * 0.5f);

			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / projectile.oldPos.Length);
				spriteBatch.Draw(texture, drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}

			return (true);
		}
	}
}
