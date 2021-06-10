using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TrelamiumTwo.Helpers;
using TrelamiumTwo.Helpers.Extensions;

namespace TrelamiumTwo.Content.Projectiles.Magic
{
	public class RukhsBlessingProjectile : ModProjectile
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
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rukh's Blessing");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[projectile.type] = 1;
		}
        public override void SetDefaults()
        {
			projectile.width = projectile.height = 16;

			projectile.magic = 
				projectile.friendly = true;
			projectile.light = .25f;
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
			}

			return false;
		}

		public override void Kill(int timeLeft)
			=> DustHelpers.SpawnDustCloud(projectile.position, projectile.width, projectile.height, 32, 10);

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height);
			Texture2D texture2D = mod.GetTexture("Assets/Glow");
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				float scale = projectile.scale * (projectile.oldPos.Length - k) / projectile.oldPos.Length * .35f;
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(Color.SandyBrown) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);

				projectile.DrawProjectileCenteredWithTexture(texture2D, spriteBatch, default);
				spriteBatch.Draw(texture2D, drawPos, null, color, projectile.rotation, drawOrigin, scale, SpriteEffects.None, 0f);
			}
			return false;
		}
	}
}
