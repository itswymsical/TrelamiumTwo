using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Content.Projectiles.Ranged
{
	public class EarthboundArrowProjectile : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
			projectile.penetrate = -1;
		}

		public override bool PreAI()
		{
			if (projectile.timeLeft <= 3)
			{
				projectile.position = projectile.Center;
				projectile.width = 16;
				projectile.height = 44;
				projectile.Center = projectile.position;
			}
			return true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
			=> StartExplosion();
		public override void OnHitPvp(Player target, int damage, bool crit)
			=> StartExplosion();

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			StartExplosion();
			return (false);
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item27, projectile.position);
			for (int i = 0; i < 40; ++i)
			{
				Dust newDust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.Earthen>(), 0, 0, 100, default, 1f)];
				newDust.fadeIn = 1f;
				newDust.noGravity = true;
				newDust.velocity *= 1.5f;

				if (Main.rand.Next(3) == 0)
				{
					newDust.scale = 1.3f;
				}
			}

			if (Main.myPlayer == projectile.owner)
			{
				for (int i = 0; i < 5; ++i)
				{
					Vector2 velocity = (-Vector2.UnitY).RotatedByRandom(MathHelper.PiOver2) * 6f;
					Projectile.NewProjectile(projectile.Center, velocity, ModContent.ProjectileType<DustiliteShard>(), 1, 0.5f, projectile.owner);
				}
			}
		}
		private void StartExplosion() 
			=> projectile.timeLeft = (projectile.timeLeft > 3) ? 3 : projectile.timeLeft;
	}
}
