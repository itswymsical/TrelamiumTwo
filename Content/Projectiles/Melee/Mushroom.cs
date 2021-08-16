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
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 16;

			Projectile.penetrate = -1;
			Projectile.aiStyle = 1;
			Projectile.timeLeft = 130;

			Projectile.DamageType = DamageClass.Melee;
			Projectile.friendly = true;
		}
		public override void AI()
		{
			int num3;
			Projectile.ai[1] += 1f;
			Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;

			if (Projectile.velocity.Y > 18f)
				Projectile.velocity.Y = 18f;
			
			Projectile.velocity.X = Projectile.velocity.X * 0.98f;

			for (int num228 = 0; num228 < 2; num228 = num3 + 1)
			{
				int num229 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.Mushroom>(), Projectile.velocity.X, Projectile.velocity.Y, 50, default, 1.1f);
				Main.dust[num229].position = (Main.dust[num229].position + Projectile.Center) / 2f;
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
				Dust.NewDustDirect(Projectile.Center, 0, 0, ModContent.DustType<Dusts.Mushroom>(), velocity.X, velocity.Y);
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[Projectile.type].Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[Projectile.type], drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
    }
}