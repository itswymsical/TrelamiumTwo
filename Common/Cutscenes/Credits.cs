using TrelamiumTwo.Common.Players;
using TrelamiumTwo.Core.Mechanics;
using TrelamiumTwo.Helpers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace TrelamiumTwo.Common.Cutscenes
{
	public class Credits : Cutscene
	{
		public int CreditsTimer;

		public enum CreditsSection
		{
			Introduction,
			Developers,
			Contributors,
			SpecialThanks,
			Ending
		}

		public CreditsSection CurrentSection;

		private float flashIntensity;

		public override void ModifyScreenPosition()
		{
			Player player = Main.LocalPlayer;
			CutscenePlayer cutscenePlayer = player.GetModPlayer<CutscenePlayer>();

			if (CurrentSection != CreditsSection.Introduction && CurrentSection != CreditsSection.Ending)
				Main.screenPosition = cutscenePlayer.CreditsScreenPosition[(int)CurrentSection - 1] - new Vector2(Main.screenWidth, Main.screenHeight) / 2f;
		}

		public override void Draw()
		{
			CreditsTimer++;

			SpriteBatch spriteBatch = Main.spriteBatch;

			var screenCenter = new Vector2(Main.screenWidth, Main.screenHeight) / 2f;
			var fontScale = 0.8f;

			var labelOffset = new Vector2(screenCenter.X, 200f);
			var defaultOffset = new Vector2(screenCenter.X, 300f);

			switch (CurrentSection)
			{
				case CreditsSection.Introduction:
					{
						Texture2D icon = ModContent.GetTexture("TrelamiumTwo/Assets/logo");
						spriteBatch.Draw(icon, new Vector2(screenCenter.X, 200f) - icon.Size() / 2f, Color.White);

						break;
					}

				case CreditsSection.Developers:
					{
						Helper.DrawText(spriteBatch, Color.White, "Developers", labelOffset);
						Helper.DrawText(spriteBatch, Color.White, "Sig", defaultOffset, 0.65f);
						Helper.DrawText(spriteBatch, Color.White, "HugeKraken", new Vector2(screenCenter.X, 320f), 0.65f);
						Helper.DrawText(spriteBatch, Color.White, "Blossom", new Vector2(screenCenter.X, 340f), 0.65f);
						Helper.DrawText(spriteBatch, Color.White, "Tiredghostdude", new Vector2(screenCenter.X, 360f), 0.65f);
						Helper.DrawText(spriteBatch, Color.White, "ToyotaAristoMoment", new Vector2(screenCenter.X, 380f), 0.65f);
						break;
					}

				case CreditsSection.Contributors:
					{
						Helper.DrawText(spriteBatch, Color.White, "Contributors", labelOffset);
						Helper.DrawText(spriteBatch, Color.White, "MountainyBear49", defaultOffset, fontScale);
						Helper.DrawText(spriteBatch, Color.White, "Andrizinho16", defaultOffset, fontScale);
						Helper.DrawText(spriteBatch, Color.White, "Pyxis", defaultOffset, fontScale);
						Helper.DrawText(spriteBatch, Color.White, "Andrizinho16", defaultOffset, fontScale);
						break;
					}

				case CreditsSection.SpecialThanks:
					{
						Helper.DrawText(spriteBatch, Color.White, "Special Thanks", labelOffset);

						string[] names =
						{
							"Endless Escapade (For some sprites)",
							"Toothbrush (For the icon)"
					};

						var namePosition = new Vector2(screenCenter.X, 300f);

						foreach (var name in names)
							Helper.DrawTextCollumn(spriteBatch, Color.White, name, ref namePosition, fontScale);

						break;
					}

				case CreditsSection.Ending:
					{
						Helper.DrawText(spriteBatch, Color.White, "And most importantly", labelOffset);

						string[] text =
						{
						"Thanks to you!",
						"Seriously, thank you so much for playing",
						"It means a lot to me and the team",
						"S2"
					};

						var textPosition = new Vector2(screenCenter.X, 300f);

						foreach (var word in text)
							Helper.DrawTextCollumn(spriteBatch, Color.White, word, ref textPosition, fontScale);

						break;
					}
			}

			// There's probably a better way to do this.
			if (CreditsTimer < 300)
				flashIntensity *= 0.95f;
			else
				HandleCreditsTransition();

			spriteBatch.Draw(Main.magicPixel, new Rectangle(-2, -2, Main.screenWidth + 4, Main.screenHeight + 4), new Rectangle(0, 0, 1, 1), Color.White * flashIntensity);
		}

		public override void End()
		{
			flashIntensity = 0f;
			CreditsTimer = 0;
			CurrentSection = CreditsSection.Introduction;

			base.End();
		}

		private void HandleCreditsTransition()
		{
			flashIntensity = MathHelper.SmoothStep(flashIntensity, 1f, 0.1f);

			if (flashIntensity >= 0.99f)
			{
				if (CurrentSection == CreditsSection.Ending)
					End();
				else
					CurrentSection++;

				CreditsTimer = 0;
			}
		}
	}
}