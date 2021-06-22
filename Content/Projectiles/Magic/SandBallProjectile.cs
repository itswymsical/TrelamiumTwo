using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

using TrelamiumTwo.Helpers;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Projectiles.Magic
{
	public class SandBall : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_" + ProjectileID.SandBallFalling;

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 25;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = projectile.height = 14;

			projectile.penetrate = 1;
			projectile.aiStyle = -1;
			projectile.extraUpdates = 1;

			projectile.alpha = 255;
			projectile.ranged = projectile.friendly = true;
			
			projectile.timeLeft = Main.rand.Next(75, 85);
			projectile.light = 0.55f;
		}
		int timer;
		public override void AI()
		{
			if (projectile.ai[0] != 1)
			{
				if (projectile.alpha > 0)
				{
					projectile.alpha -= 40;
				}
				if (projectile.alpha < 0)
				{
					projectile.alpha = 0;
				}
				projectile.velocity *= 0.98f;
				projectile.rotation = projectile.velocity.ToRotation() - 1.57079637f;
				if (Main.myPlayer == projectile.owner && projectile.timeLeft > Main.rand.Next(50, 60))
				{
					projectile.timeLeft = Main.rand.Next(50, 60);
					return;
				}
			}
			if (projectile.ai[0] == 1)
			{
				timer++;
				projectile.width = projectile.height = 36;
				
				if (timer == 2)
				{
					projectile.Kill();
				}
			}
		}
		public override void Kill(int timeLeft)
		{
			if (projectile.ai[0] == 1)
			{
				Vector2 spinningpoint = new Vector2(0f, -3f).RotatedByRandom(3.1415927410125732);
				float num = (float)Main.rand.Next(7, 8);
				Vector2 value = new Vector2(2.1f, 2f);
				for (float num2 = 0f; num2 < num; num2 += 1f)
				{
					int num3 = Dust.NewDust(projectile.position, 0, 0, 32, 0f, 0f, 0, default, 1f);
					Dust.NewDust(projectile.position, 0, 0, 32, 0f, 0f, 0, default, 1f);
					Main.dust[num3].position = projectile.Center;
					Main.dust[num3].velocity = spinningpoint.RotatedBy((double)(6.28318548f * num2 / num), default) * value * (0.8f + Main.rand.NextFloat() * 0.4f);
					Main.dust[num3].noGravity = true;
					Main.dust[num3].scale = 1f;
					if (num3 != 6000)
					{
						Dust expr_141 = Dust.CloneDust(num3);
						expr_141.scale /= 1.3f;
						expr_141.color = new Color(255, 255, 255, 255);
					}
				}
			}
			if (projectile.ai[0] != 1)
			{
				Main.PlaySound(SoundID.NPCKilled, -1, -1, 35, 0.15f);
				int i = 6;
				for (int k = 0; k < i; k++)
				{
					int DustIndex = Dust.NewDust(projectile.Center, 0, 0, 32, 0f, 0f, 0, default, 0.75f);
					Dust dust = Main.dust[DustIndex];
					dust.velocity *= 1.6f;
					Dust dust2 = Main.dust[DustIndex];
					dust2.velocity.Y =- 1f;
					Main.dust[DustIndex].position = Vector2.Lerp(Main.dust[DustIndex].position, projectile.Center, 0.75f);
				}
				if (projectile.owner == Main.myPlayer)
				{
					Vector2 value16 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
					if (Main.player[projectile.owner].gravDir == -1f)
					{
						value16.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y;
					}
					Vector2 vector22 = Vector2.Normalize(value16 - projectile.Center);
					vector22 *= 3;
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector22.X, vector22.Y, ProjectileType<SandBall>(), projectile.damage, projectile.knockBack, projectile.owner, 1f, 0f);
				}
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height);
			Texture2D texture2D = mod.GetTexture("Assets/Glow");
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				float scale = projectile.scale * (projectile.oldPos.Length - k) / projectile.oldPos.Length * .35f;
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(Color.SandyBrown) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				projectile.DrawProjectileTrailCenteredWithTexture(texture2D, spriteBatch, default);
				spriteBatch.Draw(texture2D, drawPos, null, color, projectile.rotation, drawOrigin, scale, SpriteEffects.None, 0f);
			}
			return false;
		}
	}
}