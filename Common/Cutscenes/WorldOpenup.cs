using TrelamiumTwo.Core.Mechanics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Worlds;

namespace TrelamiumTwo.Common.Cutscenes
{
	public class WorldOpenup : Cutscene
	{
		private float alpha;

		private int timer;

		public override void Draw()
		{
			SpriteBatch spriteBatch = Main.spriteBatch;

			var screenCenter = new Vector2(Main.screenWidth, Main.screenHeight) / 2f;

			timer++;

			Texture2D icon = ModContent.GetTexture("TrelamiumTwo/Assets/logo");
			var iconPosition = new Vector2(screenCenter.X, 200f) - icon.Size() / 2f;
			alpha = MathHelper.SmoothStep(alpha, timer < 300 ? 1f : 0f, 0.1f);
			spriteBatch.Draw(icon, iconPosition, Color.White * alpha);

			if (timer > 300 && alpha <= 0.01f)
				End();
		}

		public override void End()
		{
			timer = 0;
			alpha = 0f;
			TrelamiumWorld.initialCutscene = true;
			base.End();
		}
	}
}