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
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
		}
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 36;
			
			Projectile.penetrate = -1;

			Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.friendly = true;
		}

		public override bool PreAI()
		{			
			Player owner = Main.player[Projectile.owner];

			if (owner.channel)
			{
				Projectile.ai[0] += rotationSpeed;
				Projectile.Center = Vector2.Lerp(Projectile.Center, owner.Center + new Vector2((float)Math.Cos(Projectile.ai[0]) * 100f, (float)Math.Sin(Projectile.ai[0]) * 60f), 0.15f);
			}
			else
			{
				if (Projectile.localAI[0] == 0)
				{
					Projectile.localAI[0] = 1;
					Projectile.netUpdate = true;
				}

				if (++Projectile.ai[1] >= 30f)
				{
					Vector2 directionToOwner = Vector2.Normalize(owner.Center - Projectile.Center);
					Projectile.velocity = Vector2.Lerp(Projectile.velocity, directionToOwner * 8f, 0.08f);
				}
				else
				{
					Projectile.velocity = (Projectile.ai[0] + MathHelper.PiOver2).ToRotationVector2() * 10f;
				}

				if (Vector2.DistanceSquared(Projectile.Center, owner.Center) <= 20 * 20)
				{
					Projectile.Kill();
				}
			}

			owner.itemTime = owner.itemAnimation = 10;
			owner.ChangeDir((Projectile.Center.X > owner.Center.X).ToDirectionInt());
			owner.itemRotation = owner.DirectionTo(Projectile.Center).ToRotation() * Math.Sign(Projectile.Center.X - owner.Center.X);

			Projectile.rotation += Main.windSpeed / 2f + 0.1f * Math.Sign(Main.windSpeed);

			return (false);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Player owner = Main.player[Projectile.owner];

			Vector2 mountedCenter = owner.MountedCenter;
			Texture2D chainTexture = ModContent.Request<Texture2D>(Texture + "_Chain");
			Rectangle chainFrame = chainTexture.Frame(1, 1, 0, 0);

			var drawPosition = Projectile.Center;
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


			Projectile.DrawProjectileTrailCentered(spriteBatch, lightColor);
			return Projectile.DrawProjectileCentered(spriteBatch, lightColor);
		}
	}
}