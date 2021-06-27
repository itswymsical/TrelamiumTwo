using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TrelamiumTwo.Helpers;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Projectiles.Melee
{
	public class TheWalnut : ModProjectile
	{
		public override string Texture => Assets.Projectiles.Melee + "TheWalnut";
		private readonly float rotationSpeed = MathHelper.TwoPi / 50;
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
		}
		public override void SetDefaults()
		{
			projectile.width = projectile.height = 36;
			
			projectile.penetrate = -1;

			projectile.melee = true;
			projectile.friendly = true;
		}

		public override bool PreAI()
		{			
			Player owner = Main.player[projectile.owner];

			if (owner.channel)
			{
				projectile.ai[0] += rotationSpeed;
				projectile.Center = Vector2.Lerp(projectile.Center, owner.Center + new Vector2((float)Math.Cos(projectile.ai[0]) * 100f, (float)Math.Sin(projectile.ai[0]) * 60f), 0.15f);
			}
			else
			{
				if (projectile.localAI[0] == 0)
				{
					projectile.localAI[0] = 1;
					projectile.netUpdate = true;
				}

				if (++projectile.ai[1] >= 30f)
				{
					Vector2 directionToOwner = Vector2.Normalize(owner.Center - projectile.Center);
					projectile.velocity = Vector2.Lerp(projectile.velocity, directionToOwner * 8f, 0.08f);
				}
				else
				{
					projectile.velocity = (projectile.ai[0] + MathHelper.PiOver2).ToRotationVector2() * 10f;
				}

				if (Vector2.DistanceSquared(projectile.Center, owner.Center) <= 20 * 20)
				{
					projectile.Kill();
				}
			}

			owner.itemTime = owner.itemAnimation = 10;
			owner.ChangeDir((projectile.Center.X > owner.Center.X).ToDirectionInt());
			owner.itemRotation = owner.DirectionTo(projectile.Center).ToRotation() * Math.Sign(projectile.Center.X - owner.Center.X);

			projectile.rotation += Main.windSpeed / 2f + 0.1f * Math.Sign(Main.windSpeed);

			return (false);
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
			=> false;
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Player owner = Main.player[projectile.owner];

			Vector2 mountedCenter = owner.MountedCenter;
			Texture2D chainTexture = ModContent.GetTexture(Texture + "_Chain");
			Rectangle chainFrame = chainTexture.Frame(1, 1, 0, 0);

			var drawPosition = projectile.Center;
			var remainingVectorToPlayer = mountedCenter - drawPosition;

			float rotation = remainingVectorToPlayer.ToRotation();

			bool drawChain = true;
			while (drawChain)
			{
				float length = remainingVectorToPlayer.Length();

				if (float.IsNaN(length))
				{
					break;
				}

				if (length <= chainTexture.Width)
				{
					drawChain = false;
					chainFrame.Width = (int)length;
				}

				drawPosition += remainingVectorToPlayer * 12 / length;
				remainingVectorToPlayer = mountedCenter - drawPosition;

				Color color = Lighting.GetColor((int)drawPosition.X / 16, (int)(drawPosition.Y / 16f));
				spriteBatch.Draw(chainTexture, drawPosition - Main.screenPosition, chainFrame, color, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
			}


			projectile.DrawProjectileTrailCentered(spriteBatch, lightColor);
			return projectile.DrawProjectileCentered(spriteBatch, lightColor);
		}
	}
}