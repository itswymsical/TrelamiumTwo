using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

using TrelamiumTwo.Helpers;
using TrelamiumTwo.Core;
using Terraria.Audio;

namespace TrelamiumTwo.Content.Projectiles.Magic
{
	public class SandBall : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_" + ProjectileID.SandBallFalling;

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 25;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 14;

			Projectile.penetrate = 1;
			Projectile.aiStyle = -1;
			Projectile.extraUpdates = 1;

			Projectile.alpha = 255;
			Projectile.DamageType = // projectile.friendly = true;
			
			Projectile.timeLeft = Main.rand.Next(75, 85);
			Projectile.light = 0.55f;
		}
		int timer;
		public override void AI()
		{
			if (Projectile.ai[0] != 1)
			{
				if (Projectile.alpha > 0)
				{
					Projectile.alpha -= 40;
				}
				if (Projectile.alpha < 0)
				{
					Projectile.alpha = 0;
				}
				Projectile.velocity *= 0.98f;
				Projectile.rotation = Projectile.velocity.ToRotation() - 1.57079637f;
				if (Main.myPlayer == Projectile.owner && Projectile.timeLeft > Main.rand.Next(50, 60))
				{
					Projectile.timeLeft = Main.rand.Next(50, 60);
					return;
				}
			}
			if (Projectile.ai[0] == 1)
			{
				timer++;
				Projectile.width = Projectile.height = 36;
				
				if (timer == 2)
				{
					Projectile.Kill();
				}
			}
		}
		public override void Kill(int timeLeft)
		{
			if (Projectile.ai[0] == 1)
			{
				Vector2 spinningpoint = new Vector2(0f, -3f).RotatedByRandom(3.1415927410125732);
				float num = (float)Main.rand.Next(7, 8);
				Vector2 value = new Vector2(2.1f, 2f);
				for (float num2 = 0f; num2 < num; num2 += 1f)
				{
					int num3 = Dust.NewDust(Projectile.position, 0, 0, 32, 0f, 0f, 0, default, 1f);
					Dust.NewDust(Projectile.position, 0, 0, 32, 0f, 0f, 0, default, 1f);
					Main.dust[num3].position = Projectile.Center;
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
			if (Projectile.ai[0] != 1)
			{
				SoundEngine.PlaySound(SoundID.NPCKilled, -1, -1, 35, 0.15f);
				int i = 6;
				for (int k = 0; k < i; k++)
				{
					int DustIndex = Dust.NewDust(Projectile.Center, 0, 0, 32, 0f, 0f, 0, default, 0.75f);
					Dust dust = Main.dust[DustIndex];
					dust.velocity *= 1.6f;
					Dust dust2 = Main.dust[DustIndex];
					dust2.velocity.Y =- 1f;
					Main.dust[DustIndex].position = Vector2.Lerp(Main.dust[DustIndex].position, Projectile.Center, 0.75f);
				}
				if (Projectile.owner == Main.myPlayer)
				{
					Vector2 value16 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
					if (Main.player[Projectile.owner].gravDir == -1f)
					{
						value16.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y;
					}
					Vector2 vector22 = Vector2.Normalize(value16 - Projectile.Center);
					vector22 *= 3;
					Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, vector22.X, vector22.Y, ProjectileType<SandBall>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 1f, 0f);
				}
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture2D = Mod.Assets.Request<Texture2D>("Assets/Glow").Value;
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				float scale = (Projectile.scale - .1f) * (Projectile.oldPos.Length - k) / Projectile.oldPos.Length * .45f;
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + Main.projectileTexture[Projectile.type].Size() / 2f;
				Color color = Projectile.GetAlpha(Color.SandyBrown) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);

				spriteBatch.Draw(texture2D, drawPos, null, color, Projectile.rotation, Main.projectileTexture[Projectile.type].Size(), scale, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}