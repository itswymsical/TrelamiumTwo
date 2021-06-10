using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TrelamiumTwo.Common.Extensions;

namespace TrelamiumTwo.Content.Projectiles.Magic
{
	public class MagicAcorn : ModProjectile
	{
		private enum AIState
		{
			Charging = 0,
			Shooting = 1
		}

		private AIState State
		{
			get => (AIState)projectile.ai[0];
			set => projectile.ai[0] = (int)value;
		}

		private Vector2 TargetPosition;
		private readonly float MaxChargeTimer = 60f;

		public override void SetDefaults()
		{
			projectile.width = projectile.height = 12;

			projectile.magic = true;
			projectile.friendly = true;

			TargetPosition = Vector2.Zero;
		}

		public override bool PreAI()
		{
			if (projectile.localAI[0] == 0)
			{
				projectile.localAI[0] = 1;

				TargetPosition = new Vector2(projectile.ai[0], projectile.ai[1]) - projectile.Center;
				TargetPosition.Normalize();

				projectile.ai[0] = projectile.ai[1] = 0;
			}

			if (State == AIState.Charging)
			{
				projectile.ai[1]++;
				float modifier = projectile.ai[1] / MaxChargeTimer;

				projectile.Opacity = modifier;
				projectile.rotation += modifier * 0.5f;

				projectile.velocity = Vector2.Zero;
				if (Main.myPlayer == projectile.owner && projectile.ai[1] >= MaxChargeTimer)
				{
					projectile.netUpdate = true;

					projectile.ai[1] = 0;
					State = AIState.Shooting;
					projectile.velocity = TargetPosition * 10;
				}
			}
			else
			{
				if (++projectile.ai[1] >= 15)
				{
					projectile.velocity.Y += 0.2f;
					projectile.velocity.X *= 0.99f;
				}

				projectile.rotation += projectile.velocity.Length() * 0.2f;
			}

			return false;
		}

		public override void Kill(int timeLeft)
			=> Helpers.DustHelpers.SpawnDustCloud(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.Wood>(), 10);

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
			=> this.DrawProjectileCentered(spriteBatch, lightColor * projectile.Opacity);
	}
}
