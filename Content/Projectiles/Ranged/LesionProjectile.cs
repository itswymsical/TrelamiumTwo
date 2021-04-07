using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Trelamium2.Content.Projectiles.Ranged
{
	public class LesionProjectile : ModProjectile
	{
        public override string Texture => Trelamium2.PLACEHOLDER_TEXTURE;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lesion");
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.penetrate = 1;
			projectile.aiStyle = 1;

			projectile.alpha = 255;
			projectile.hide = true;

			projectile.timeLeft = 260;
			projectile.ranged = true;
			projectile.friendly = true;
		}
		public override void AI()
		{
			int num3;
			projectile.ai[1] += 1f;
			float num227 = (60f - projectile.ai[1]) / 60f;
			if (projectile.ai[1] > 40f)
			{
				projectile.Kill();
			}
			projectile.velocity.Y = projectile.velocity.Y + 0.2f;
			if (projectile.velocity.Y > 18f)
			{
				projectile.velocity.Y = 18f;
			}
			projectile.velocity.X = projectile.velocity.X * 0.98f;
			for (int num228 = 0; num228 < 2; num228 = num3 + 1)
			{
				int num229 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 
					4, projectile.velocity.X, projectile.velocity.Y, 50, Color.OrangeRed, 1.1f);
				Main.dust[num229].position = (Main.dust[num229].position + projectile.Center) / 2f;
				Main.dust[num229].noGravity = true;
				Dust dust = Main.dust[num229];
				dust.velocity *= 0.3f;
				num3 = num228;
			}
			for (int num230 = 0; num230 < 1; num230 = num3 + 1)
			{
				int num229 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 
					4, projectile.velocity.X, projectile.velocity.Y, 50, Color.OrangeRed, 0.6f);
				Main.dust[num229].position = (Main.dust[num229].position + projectile.Center * 5f) / 6f;
				Dust dust = Main.dust[num229];
				dust.velocity *= 0.1f;
				Main.dust[num229].noGravity = true;
				num3 = num230;
			}
			return;
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if (Main.rand.NextBool(7))
				target.AddBuff(BuffID.Bleeding, 80);
        }
        public override void Kill(int timeLeft)
		{
			int num281 = 22;
			for (int num282 = 0; num282 < num281; num282++)
			{
				int num283 = Dust.NewDust(projectile.Center, 0, 0, 
					4, 0f, 0f, 0, Color.OrangeRed, 0.5f);
				Dust dust = Main.dust[num283];
				dust.velocity *= 1.6f;
				Dust dust25 = Main.dust[num283];
				dust25.velocity.Y = dust25.velocity.Y - 1f;
				Main.dust[num283].position = Vector2.Lerp(Main.dust[num283].position, projectile.Center, 0.5f);
			}
			if (Main.rand.NextBool(3))
				Main.PlaySound(SoundID.NPCKilled, -1, -1, 11, 0.35f);
		}
	}
}