using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Content.Projectiles.Ranged
{
	public sealed class NutRocketProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			projectile.width = projectile.height = 18;
			
			projectile.aiStyle = 1;
			projectile.timeLeft = 46;
			
			projectile.ranged = true;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
		}

		public override void AI()
		{
			projectile.velocity.X *= 0.994f;
			projectile.velocity.Y += 0.26f;
		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 16f, Main.rand.Next(-7, 11) * .25f, Main.rand.Next(-3, 5) * .25f, ModContent.ProjectileType<NutProjectile>(), (int)(projectile.damage * .5f), 0, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 16f, Main.rand.Next(-10, 14) * .25f, Main.rand.Next(-6, 10) * .25f, ModContent.ProjectileType<NutProjectile>(), (int)(projectile.damage * .5f), 0, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 16f, Main.rand.Next(-15, 13) * .25f, Main.rand.Next(-10, -5) * .25f, ModContent.ProjectileType<NutProjectile>(), (int)(projectile.damage * .5f), 0, projectile.owner);

			Main.PlaySound(SoundID.Dig, projectile.position);
			for (int i = 0; i < 32; ++i)
			{
				float random = Main.rand.NextFloat(-6f, 6f);
				Dust dust = Dust.NewDustDirect(projectile.Center, 0, 0, 78, random, random);
				dust.scale = 0.8f;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.projectileTexture[projectile.type];
			Rectangle rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
			Color color = Color.Lerp(Color.Red, Color.Pink, 0.5f + (float)Math.Sin(MathHelper.ToRadians(projectile.frame)) / 2f) * 0.5f;
			for (int i = 0; i < projectile.oldPos.Length; i++)
			{
				Main.spriteBatch.Draw(texture, projectile.oldPos[i] + projectile.Size / 2f - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(rectangle), color, projectile.oldRot[i], rectangle.Size() / 2f, 1f, projectile.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			}

			return (true);
		}
	}
}