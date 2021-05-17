using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI.Chat;

namespace TrelamiumTwo.Utilities
{
	internal static class DrawUtils
	{
		// TODO - Refactor

		public static void DrawSingleText(SpriteBatch spriteBatch, Color color, string text, float yOffset = 0f, float scale = 1f)
		{
			var screenCenter = new Vector2(Main.screenWidth, Main.screenHeight) / 2f;

			var font = Main.fontDeathText;

			var textSize = font.MeasureString(text) * scale;
			var textPosition = new Vector2(screenCenter.X, yOffset) - textSize / 2f;

			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, font, text, textPosition, color, 0f, default, Vector2.One * scale);
		}

		public static void DrawTextCollumn(SpriteBatch spriteBatch, Color color, string text, ref Vector2 position, float scale = 1f)
		{
			float spacement = 5f;

			var font = Main.fontDeathText;

			var textSize = font.MeasureString(text) * scale;
			var textPosition = position - textSize / 2f;
			position.Y += textSize.Y + spacement;

			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, font, text, textPosition, color, 0f, default, new Vector2(scale));
		}
	}
}
