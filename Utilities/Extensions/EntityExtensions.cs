using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace TrelamiumTwo.Utilities.Extensions
{
    internal static class EntityExtensions
    {
        public static Vector2 MoveTowardsEntity(this Entity entity, Entity target, float speed = 1f)
        {
            var direction = entity.DirectionTo(target.Center);
            var velocity = direction * speed;

            return entity.velocity += velocity;
        }

        public static Vector2 MoveAwayFromEntity(this Entity entity, Entity target, float speed = 1f)
        {
            var direction = target.DirectionTo(entity.Center);
            var velocity = direction * speed;

            return entity.velocity += velocity;
        }

        public static bool DrawEntityCenteredWithTexture(this Entity entity, Texture2D texture, SpriteBatch spriteBatch, Color lightColor)
        {
            if (entity is Projectile projectile)
            {
                Rectangle frame = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
                Vector2 origin = frame.Size() / 2 + new Vector2(projectile.modProjectile.drawOriginOffsetX, projectile.modProjectile.drawOriginOffsetY);;
                
				SpriteEffects effects = projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

                Vector2 drawPosition = projectile.Center - Main.screenPosition + new Vector2(projectile.modProjectile.drawOffsetX, 0);

                spriteBatch.Draw(texture, drawPosition, frame, lightColor, projectile.rotation, origin, projectile.scale, effects, 0f);
            }

            return false;
        }

        public static bool DrawEntityCentered(this Entity entity, SpriteBatch spriteBatch, Color lightColor)
        {
            if (entity is Projectile projectile)
            {
                Texture2D texture = Main.projectileTexture[projectile.type];

                return projectile.DrawEntityCenteredWithTexture(texture, spriteBatch, lightColor);
            }

            return false;
        }

        public static void DrawEntityTrailCenteredWithTexture(this Entity entity, Texture2D texture, SpriteBatch spriteBatch, Color drawColor, float initialOpacity = 0.8f, float opacityDegrade = 0.2f, int stepSize = 1)
        {
            if (entity is Projectile projectile)
            {
                Rectangle frame = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
                Vector2 origin = frame.Size() / 2;

                SpriteEffects effects = projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

                for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i += stepSize)
                {
                    float opacity = initialOpacity - opacityDegrade * i;
                    spriteBatch.Draw(texture, projectile.oldPos[i] + projectile.Hitbox.Size() / 2 - Main.screenPosition, frame, drawColor * opacity, projectile.oldRot[i], origin, projectile.scale, effects, 0f);
                }
            }
        }

        public static void DrawEntityTrailCentered(this Entity entity, SpriteBatch spriteBatch, Color drawColor, float initialOpacity = 0.8f, float opacityDegrade = 0.2f, int stepSize = 1)
        {
            if (entity is Projectile projectile)
            {
                Texture2D texture = Main.projectileTexture[projectile.type];
                
                projectile.DrawEntityTrailCenteredWithTexture(texture, spriteBatch, drawColor, initialOpacity, opacityDegrade, stepSize);
            }
        }
    }
}
