using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;
using TrelamiumTwo.Helpers;

namespace TrelamiumTwo.Content.Projectiles.Melee
{
	public class Mushroom : ModProjectile
	{
		public override string Texture => Assets.Projectiles.Melee + "Mushroom";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mushroom");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = projectile.height = 16;

			projectile.penetrate = -1;
			projectile.aiStyle = 1;
			projectile.timeLeft = 130;

			projectile.melee = projectile.friendly = true;
		}
		public override void AI()
		{
			int num3;
			projectile.ai[1] += 1f;
			projectile.velocity.Y = projectile.velocity.Y + 0.2f;

			if (projectile.velocity.Y > 18f)
				projectile.velocity.Y = 18f;
			
			projectile.velocity.X = projectile.velocity.X * 0.98f;

			for (int num228 = 0; num228 < 2; num228 = num3 + 1)
			{
				int num229 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.Mushroom>(), projectile.velocity.X, projectile.velocity.Y, 50, default, 1.1f);
				Main.dust[num229].position = (Main.dust[num229].position + projectile.Center) / 2f;
				Main.dust[num229].noGravity = true;
				Dust dust = Main.dust[num229];
				dust.velocity *= 0.3f;
				dust = Main.dust[num229];
				dust.scale = .45f;
				num3 = num228;
			}
			return;
		}
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 16; i++)
			{
				var velocity = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f));
				Dust.NewDustDirect(projectile.Center, 0, 0, ModContent.DustType<Dusts.Mushroom>(), velocity.X, velocity.Y);
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
    }
}