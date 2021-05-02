#region Using directives

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TrelamiumTwo.Common.Extensions;

#endregion

namespace TrelamiumTwo.Content.Projectiles.Melee
{
	public sealed class DustiliteClaymoreShard : ModProjectile
	{
		public override string Texture => TrelamiumTwo.DustiliteAssets + "DustiliteShards";

		private enum AIState
		{
			Charging = 0,
			Shooting = 1
		}
		private AIState State
		{
			get => (AIState)projectile.localAI[1];
			set => projectile.localAI[1] = (int)value;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shard");
			Main.projFrames[projectile.type] = 3;

			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			projectile.width = projectile.height = 16;

			projectile.alpha = 255;

			projectile.melee = projectile.friendly = true;
			
			projectile.tileCollide = false;
		}

		public override bool PreAI()
		{
			projectile.alpha -= 20;
			if (projectile.alpha < 0)
			{
				projectile.alpha = 0;
			}

			if (State == AIState.Charging)
			{
				if (projectile.localAI[0] == 0)
				{
					projectile.frame = Main.rand.Next(Main.projFrames[projectile.type]);
				}

				projectile.rotation += projectile.velocity.Length() * 0.1f * projectile.direction;

				if (++projectile.localAI[0] >= 10)
				{
					projectile.velocity *= 0.9f;

					if (projectile.localAI[0] >= 30)
					{
						State = AIState.Shooting;
						projectile.localAI[0] = 0;
						projectile.netUpdate = true;
						projectile.velocity = new Vector2(-projectile.ai[0], -projectile.ai[1]);
					}
				}
			}
			else
			{
				if (projectile.timeLeft > 30)
				{
					projectile.timeLeft = 30;
				}
				projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
			}

			return (false);
		}

		public override bool CanDamage()
			=> State == AIState.Shooting;

		public override void Kill(int timeLeft)
			=> Core.Utils.DustUtils.SpawnDustCloud(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.Dustilite>());

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (State == AIState.Shooting)
			{
				this.DrawProjectileTrailCentered(spriteBatch, lightColor, projectile.Opacity - 0.4f, 0.15f);
			}

			return this.DrawProjectileCentered(spriteBatch, lightColor * projectile.Opacity);
		}
	}
}
