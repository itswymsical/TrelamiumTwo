using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using TrelamiumTwo.Core;
using Terraria.Audio;

namespace TrelamiumTwo.Content.Projectiles.Ranged
{
	public class NutRocketProjectile : ModProjectile
	{
		public override string Texture => Assets.Projectiles.Ranged + "NutRocketProjectile";
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 18;
			
			Projectile.aiStyle = 1;
			Projectile.timeLeft = 46;

			Projectile.DamageType = DamageClass.Ranged;
				Projectile.friendly = 
				Projectile.ignoreWater =
				Projectile.tileCollide = true;
		}

		public override void AI()
		{
			Projectile.velocity.X *= 0.994f;
			Projectile.velocity.Y += 0.26f;
		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y - 16f, Main.rand.Next(-7, 11) * .25f, Main.rand.Next(-3, 5) * .25f, ModContent.ProjectileType<NutProjectile>(), (int)(Projectile.damage * .5f), 0, Projectile.owner);
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y - 16f, Main.rand.Next(-10, 14) * .25f, Main.rand.Next(-6, 10) * .25f, ModContent.ProjectileType<NutProjectile>(), (int)(Projectile.damage * .5f), 0, Projectile.owner);
			Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y - 16f, Main.rand.Next(-15, 13) * .25f, Main.rand.Next(-10, -5) * .25f, ModContent.ProjectileType<NutProjectile>(), (int)(Projectile.damage * .5f), 0, Projectile.owner);

			SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
			for (int i = 0; i < 32; ++i)
			{
				float random = Main.rand.NextFloat(-6f, 6f);
				Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, 78, random, random);
				dust.scale = 0.8f;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.projectileTexture[Projectile.type];
			Rectangle rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
			Color color = Color.Lerp(Color.Red, Color.Pink, 0.5f + (float)Math.Sin(MathHelper.ToRadians(Projectile.frame)) / 2f) * 0.5f;
			for (int i = 0; i < Projectile.oldPos.Length; i++)
			{
				Main.spriteBatch.Draw(texture, Projectile.oldPos[i] + Projectile.Size / 2f - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(rectangle), color, Projectile.oldRot[i], rectangle.Size() / 2f, 1f, Projectile.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}