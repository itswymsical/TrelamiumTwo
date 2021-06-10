using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;

namespace TrelamiumTwo.Helpers.Extensions
{
	internal static class ProjectileExtensions
	{
		public static bool DrawProjectileCenteredWithTexture(this Projectile projectile, Texture2D texture, SpriteBatch spriteBatch, Color color)
		{
			Rectangle frame = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);

			Vector2 origin = frame.Size() / 2f + new Vector2(projectile.modProjectile.drawOriginOffsetX, projectile.modProjectile.drawOriginOffsetY);

			SpriteEffects effects = projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

			Vector2 drawPosition = projectile.Center - Main.screenPosition + new Vector2(projectile.modProjectile.drawOffsetX, projectile.gfxOffY);

			spriteBatch.Draw(texture, drawPosition, frame, color, projectile.rotation, origin, projectile.scale, effects, 0f);

			return false;
		}

		public static bool DrawProjectileCentered(this Projectile projectile, SpriteBatch spriteBatch, Color color)
		{
			Texture2D texture = Main.projectileTexture[projectile.type];

			return projectile.DrawProjectileCenteredWithTexture(texture, spriteBatch, projectile.GetAlpha(color));
		}

		public static void DrawProjectileTrailCenteredWithTexture(this Projectile projectile, Texture2D texture, SpriteBatch spriteBatch, Color color, float initialOpacity = 0.8f, float opacityDegrade = 0.2f, int stepSize = 1)
		{
			Rectangle frame = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);

			Vector2 origin = frame.Size() / 2f;

			SpriteEffects effects = projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

			for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i += stepSize)
			{
				float opacity = initialOpacity - opacityDegrade * i;

				Vector2 position = projectile.oldPos[i] + projectile.Hitbox.Size() / 2f - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);

				spriteBatch.Draw(texture, position, frame, color * opacity, projectile.oldRot[i], origin, projectile.scale, effects, 0f);
			}
		}

		public static void DrawProjectileTrailCentered(this Projectile projectile, SpriteBatch spriteBatch, Color color, float initialOpacity = 0.8f, float opacityDegrade = 0.2f, int stepSize = 1)
		{
			Texture2D texture = Main.projectileTexture[projectile.type];

			projectile.DrawProjectileTrailCenteredWithTexture(texture, spriteBatch, projectile.GetAlpha(color), initialOpacity, opacityDegrade, stepSize);
		}
	}
}
