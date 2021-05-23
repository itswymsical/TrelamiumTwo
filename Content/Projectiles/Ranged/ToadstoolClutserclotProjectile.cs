using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Projectiles;
using TrelamiumTwo.Utilities.Extensions;

namespace TrelamiumTwo.Content.Projectiles.Ranged
{
	public class ToadstoolClutserclotProjectile : StickyProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Toadstool Clusterclot");
		}

		public override void SetDefaults()
		{
			projectile.friendly = true;
			projectile.tileCollide = true;

			stickToTile = true;
			stickToNPC = true;

			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;

			projectile.timeLeft = 280;
			projectile.penetrate = -1;
			projectile.aiStyle = 0;
		}
		public override void AI()
		{
			if (!stickingToNPC)
			{
				projectile.ai[0]++;

				if (projectile.ai[0] >= 20f)
					projectile.velocity.Y += 0.2f;

				projectile.rotation += (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) / 25f * projectile.direction;
			}

			if (Main.LocalPlayer.GetModPlayer<Common.Players.TrelamiumPlayer>().toadstoolExplode)
            {
				projectile.Kill();
            }
			if (projectile.timeLeft < 255 / 5)
				projectile.alpha += 5;

			base.AI();
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (!stickingToTile)
			{
				Main.PlaySound(SoundID.Dig, projectile.position);
				Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
			}

			return base.OnTileCollide(oldVelocity);
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			projectile.DrawProjectileTrailCentered(spriteBatch, drawColor);

			return projectile.DrawProjectileCentered(spriteBatch, drawColor);
		}
		public override void Kill(int timeLeft)
		{
			int num281 = 22;
			for (int num282 = 0; num282 < num281; num282++)
			{
				int num283 = Dust.NewDust(projectile.Center, 0, 0, 4, 0f, 0f, 0, Color.Orange, 0.5f);
				Dust dust = Main.dust[num283];
				dust.velocity *= 1.6f;
				Dust dust25 = Main.dust[num283];
				dust25.velocity.Y =  1f;
				Main.dust[num283].position = Vector2.Lerp(Main.dust[num283].position, projectile.Center, 0.5f);
			}
			for (int i = 0; i < 2; ++i)
			{
				Projectile.NewProjectile(projectile.Center, -Vector2.UnitY.RotatedByRandom(MathHelper.PiOver2) * 4f,
					ModContent.ProjectileType<ToadstoolProjectile>(), (int)(projectile.damage * 0.5f), 0.5f, projectile.owner);
			}
		}
	}
}