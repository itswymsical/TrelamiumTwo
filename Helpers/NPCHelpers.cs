using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace TrelamiumTwo.Helpers
{
    internal static partial class Helper
    {
		public static void Kill(this NPC npc, bool hitEffect = true)
		{
			if (npc.modNPC?.CheckDead() == false)
			{
				return;
			}

			npc.life = 0;

			if (hitEffect)
			{
				npc.HitEffect();
			}

			npc.checkDead();

			npc.active = false;
		}

		public static bool DrawNPCCenteredWithTexture(this NPC npc, Texture2D texture, SpriteBatch spriteBatch, Color color)
		{
			Vector2 origin = npc.frame.Size() / 2f + new Vector2(0f, npc.modNPC.drawOffsetY);

			SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

			Vector2 drawPosition = npc.Center.ToDrawPosition() + new Vector2(0f, npc.gfxOffY);

			spriteBatch.Draw(texture, drawPosition, npc.frame, color, npc.rotation, origin, npc.scale, effects, 0f);

			return false;
		}

		public static bool DrawNPCCentered(this NPC npc, SpriteBatch spriteBatch, Color color)
		{
			Texture2D texture = Main.npcTexture[npc.type];

			return npc.DrawNPCCenteredWithTexture(texture, spriteBatch, npc.GetAlpha(color));
		}

		public static void DrawNPCTrailCenteredWithTexture(this NPC npc, Texture2D texture, SpriteBatch spriteBatch, Color color, float initialOpacity = 0.8f, float opacityDegrade = 0.2f, int stepSize = 1)
		{
			Vector2 origin = npc.frame.Size() / 2f;

			SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

			for (int i = 0; i < NPCID.Sets.TrailCacheLength[npc.type]; i += stepSize)
			{
				float opacity = initialOpacity - opacityDegrade * i;

				Vector2 position = npc.oldPos[i].ToDrawPosition() + npc.Hitbox.Size() / 2f + new Vector2(0f, npc.gfxOffY);

				spriteBatch.Draw(texture, position, npc.frame, color * opacity, npc.oldRot[i], origin, npc.scale, effects, 0f);
			}
		}

		public static void DrawNPCTrailCentered(this NPC npc, SpriteBatch spriteBatch, Color color, float initialOpacity = 0.8f, float opacityDegrade = 0.2f, int stepSize = 1)
		{
			Texture2D texture = Main.npcTexture[npc.type];

			npc.DrawNPCTrailCenteredWithTexture(texture, spriteBatch, npc.GetAlpha(color), initialOpacity, opacityDegrade, stepSize);
		}
	}
}
