using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using TrelamiumTwo.Common.Players;
using TrelamiumTwo.Core.Abstracts;
using TrelamiumTwo.Core.Utils;

namespace TrelamiumTwo.Common.Cutscenes
{
	public class Glacier_Intro : Cutscene
	{
		public override int InsertionIndex(List<GameInterfaceLayer> layers) => layers.FindIndex(l => l.Name.Equals("Vanilla: Mouse Text"));

		public int CutsceneTimer;

		public enum CreditsSection
		{
			Play,
			End
		}
		public CreditsSection CurrentSection;
		private float flashIntensity;

		public override void ModifyScreenPosition()
		{
			Player player = Main.LocalPlayer;
			CutscenePlayer cutscenePlayer = player.GetModPlayer<CutscenePlayer>();
			if (CurrentSection != CreditsSection.Play && CurrentSection != CreditsSection.End)
				Main.screenPosition = cutscenePlayer.CreditsScreenPosition[(int)CurrentSection - 1] - new Vector2(Main.screenWidth, Main.screenHeight) / 2f;
		}
		public override void Draw()
		{
			CutsceneTimer++;

			SpriteBatch spriteBatch = Main.spriteBatch;

			switch (CurrentSection)
			{
				case CreditsSection.Play:
				{
					DrawUtils.DrawSingleText(spriteBatch, Color.White, "-- Glacier --", Main.rand.NextFloat(298, 300f));
					DrawUtils.DrawSingleText(spriteBatch, Color.White, "Frostbound Mage", Main.rand.NextFloat(166f, 170f));

					break;
				}
			}

			if (CutsceneTimer < 200)
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
				if (CurrentSection == CreditsSection.End)
					Visible = false;
				else
					CurrentSection++;

				CutsceneTimer = 0;
			}
		}
	}
}