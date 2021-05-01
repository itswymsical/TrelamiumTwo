using TrelamiumTwo.Common.Players;
using TrelamiumTwo.Core.Abstracts;
using TrelamiumTwo.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace TrelamiumTwo.Common.Cutscenes
{
	public class Credits : Cutscene
	{
		public override int InsertionIndex(List<GameInterfaceLayer> layers) => layers.FindIndex(l => l.Name.Equals("Vanilla: Mouse Text"));

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

			switch (CurrentSection)
			{
				case CreditsSection.Introduction:
				{
					Texture2D icon = ModContent.GetTexture("TrelamiumTwo/icon");
					spriteBatch.Draw(icon, new Vector2(screenCenter.X, 200f) - icon.Size() / 2f, Color.White);

					DrawUtils.DrawSingleText(spriteBatch, Color.White, "Trelamium 2", 300f);

					break;
				}

				case CreditsSection.Developers:
				{
					DrawUtils.DrawSingleText(spriteBatch, Color.White, "Developers", 100f);
						string[] text =
					{
						"Signia (sig)",
						"HugeKraken",
						"Blossom",
						"MountainyBear49",
						"Tiredghostdude",
						"Lemmy",
						"musicman",
						"iFlicky",
						"OneDiamondBoi",
						"ShockedHorizon5",
						"HondaCivicMoment",
						"Lucia",
						"RighteousRyan",
						"Tobias.",
						"Pyxis"
					};

					var textPosition = new Vector2(screenCenter.X, 200f);

					foreach (var word in text)
						DrawUtils.DrawTextCollumn(spriteBatch, Color.White, word, ref textPosition, fontScale);
					break;
				}
				case CreditsSection.Contributors:
				{
					DrawUtils.DrawSingleText(spriteBatch, Color.White, "Contributors", 200f);
					string[] text =
					{
						"naka",
						"Arcri",
						"ExitiumTheCat",
						"TacoBurritoGuacamole",
						"Jax",
						"Maskano"
					};

					var textPosition = new Vector2(screenCenter.X, 300f);

					foreach (var word in text)
						DrawUtils.DrawTextCollumn(spriteBatch, Color.White, word, ref textPosition, fontScale);
					break;
				}
				case CreditsSection.SpecialThanks:
				{
					DrawUtils.DrawSingleText(spriteBatch, Color.White, "Special Thanks", 200f);

					string[] names =
					{
							"naka (Cutscene Loader)",
							"Primordial Sands & Etheral Horizons Developers for tagging along with the mod merges."
					};

					var namePosition = new Vector2(screenCenter.X, 300f);

					foreach (var name in names)
						DrawUtils.DrawTextCollumn(spriteBatch, Color.White, name, ref namePosition, fontScale);

					break;
				}
				case CreditsSection.Ending:
				{
					DrawUtils.DrawSingleText(spriteBatch, Color.White, "Thank you for Playing Trelamium 2!", 200f);

					string[] text =
					{
						"Make sure to join the discord server if you haven't!",
						"Link is inside mod description (https://discord.gg/HmZ6GUP3mm)"
					};

					var textPosition = new Vector2(screenCenter.X, 300f);

					foreach (var word in text)
						DrawUtils.DrawTextCollumn(spriteBatch, Color.White, word, ref textPosition, fontScale);

					break;
				}
			}
			if (CreditsTimer < 300)
				flashIntensity *= 0.95f;
			else
				HandleCreditsTransition();

			spriteBatch.Draw(Main.magicPixel, new Rectangle(-2, -2, Main.screenWidth + 4, Main.screenHeight + 4), new Rectangle(0, 0, 1, 1), Color.White * flashIntensity);
		}

		private void HandleCreditsTransition()
		{
			flashIntensity = MathHelper.SmoothStep(flashIntensity, 1f, 0.1f);

			if (flashIntensity >= 0.99f)
			{
				if (CurrentSection == CreditsSection.Ending)
					Visible = false;
				else
					CurrentSection++;

				CreditsTimer = 0;
			}
		}
	}
}
