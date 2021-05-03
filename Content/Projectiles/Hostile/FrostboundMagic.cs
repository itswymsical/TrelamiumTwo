using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Projectiles.Hostile
{
	public class FrostboundMagic : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_" + ProjectileID.None;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frostbound Magic");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;
			projectile.penetrate = 1;
			projectile.aiStyle = -1;
			projectile.timeLeft = 600;
			projectile.extraUpdates = 1;
			projectile.thrown = true;
			projectile.friendly = true;
			projectile.light = 0.5f;
		}
		public override void Kill(int timeLeft)
		{
			int num281 = 25;
			for (int num282 = 0; num282 < num281; num282++)
			{
				int num283 = Dust.NewDust(projectile.Center, 0, 0, 181, 0f, 0f, 0, default, 1f);
				Dust dust = Main.dust[num283];
				dust.velocity *= 1.6f;
				Dust dust25 = Main.dust[num283];
				dust25.velocity.Y -= dust25.velocity.Y - 1f;
				Main.dust[num283].position = Vector2.Lerp(Main.dust[num283].position, projectile.Center, 1f);
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width, projectile.height);
			Texture2D texture2D = mod.GetTexture("Assets/Glow");

			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				float scale = projectile.scale * (projectile.oldPos.Length - k) / projectile.oldPos.Length * 1f;
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin;
				Color color = projectile.GetAlpha(new Color(13, 87, 189)) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);

				spriteBatch.Draw(texture2D, drawPos, null, color, projectile.rotation, drawOrigin, scale, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}