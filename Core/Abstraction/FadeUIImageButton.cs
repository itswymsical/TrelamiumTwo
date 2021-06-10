using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace TrelamiumTwo.Core.Abstraction
{
	// Base code by Stevie (convicted tomatophile), all credits goes to them.

	public class FadeUIImageButton : UIElement
	{
		public float lerpAmount = 0.1f;
		public float lerpVisibility, visibilityInactive = 0.4f;
		public float visibilityActive = 1f;

		public FadeUIImageButton(Texture2D buttonTexture, string drawText = "") => SetImage(buttonTexture, drawText ?? string.Empty);

		public Texture2D ButtonTexture { get; private set; }

		public string DrawText { get; private set; }

		public void SetImage(Texture2D buttonTexture, string drawText = "")
		{
			ButtonTexture = buttonTexture;
			DrawText = drawText;

			Width.Set(ButtonTexture.Width, 0f);
			Height.Set(ButtonTexture.Height, 0f);
		}

		override protected void DrawSelf(SpriteBatch spriteBatch)
		{
			Vector2 drawPos = GetDimensions().Position();
			Color drawColor = Color.White * lerpVisibility;

			spriteBatch.Draw(ButtonTexture, drawPos, drawColor);

			if (IsMouseHovering)
				UICommon.DrawHoverStringInBounds(spriteBatch, DrawText, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight));
		}

		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);

			Main.PlaySound(SoundID.MenuTick);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			lerpVisibility = MathHelper.Lerp(lerpVisibility, IsMouseHovering ? visibilityActive : visibilityInactive, lerpAmount);
		}
	}
}
