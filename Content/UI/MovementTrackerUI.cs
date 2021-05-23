#region Using Directives
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
#endregion

namespace TrelamiumTwo.Content.UI
{
	internal class MovementTrackerUI : UIState
	{
		private UIText text;
		private UIElement uIElement;
		private UIImage frame;

		public override void OnInitialize() {
			uIElement = new UIElement();
			uIElement.Left.Set(-uIElement.Width.Pixels - 600, 1f);
			uIElement.Top.Set(30, 0f);
			uIElement.Width.Set(182, 0f);
			uIElement.Height.Set(60, 0f);

			frame = new UIImage(GetTexture("Terraria/Item_" + ItemID.HermesBoots));
			frame.Left.Set(22, 0f);
			frame.Top.Set(0, 0f);
			frame.Width.Set(138, 0f);
			frame.Height.Set(32, 0f);

			text = new UIText("0/0", 0.8f);
			text.Width.Set(138, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(40, 0f);
			text.Left.Set(0, 0f);

			uIElement.Append(text);
			uIElement.Append(frame);
			Append(uIElement);
		}
		public override void Draw(SpriteBatch spriteBatch)
        {
			if (!(Main.playerInventory))
				return;
			base.Draw(spriteBatch);
		}
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			var player = Main.LocalPlayer;
			float quotient = player.moveSpeed / player.maxRunSpeed;
			quotient = Utils.Clamp(quotient, 0f, 1f);
			Rectangle hitbox = frame.GetInnerDimensions().ToRectangle();
			hitbox.X += 12;
			hitbox.Width -= 24;
			hitbox.Y += 8;
			hitbox.Height -= 16;

			int left = hitbox.Left;
			int right = hitbox.Right;
			int steps = (int)((right - left) * quotient);
			for (int i = 0; i < steps; i += 1)
			{
				spriteBatch.Draw(Main.magicPixel, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), default);
			}
		}
        public override void Update(GameTime gameTime)
		{
			text.SetText($"{Main.LocalPlayer.moveSpeed}x max acceleration speed");
			base.Update(gameTime);
		}
	}
}
