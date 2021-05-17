using TrelamiumTwo.Common.Configuration;
using TrelamiumTwo.Core.Abstracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace TrelamiumTwo.Content.UI
{
	public class DiscordInviteUI : SmartUIState
	{
		public override int InsertionIndex(List<GameInterfaceLayer> layers) => layers.FindIndex(l => l.Name.Equals("Vanilla: Mouse Text"));

		private FadeUIImageButton imageButton;

		private readonly Texture2D iconTexture = ModContent.GetTexture("TrelamiumTwo/icon");

		private readonly string discordInvite = "https://discord.gg/HmZ6GUP3mm";

		public override void OnInitialize()
		{
			imageButton = new FadeUIImageButton(iconTexture, "Discord Server");
			imageButton.Width.Set(iconTexture.Width, 0f);
			imageButton.Height.Set(iconTexture.Height, 0f);

			imageButton.Left.Set(20f, 0f);
			imageButton.Top.Set(20f, 0f);

			imageButton.OnClick += (a, b) => Process.Start(discordInvite);

			Append(imageButton);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!Main.gameMenu || !TrelamiumClientConfig.Instance.DiscordInvite)
				return;

			base.Draw(spriteBatch);
		}

		public override void Update(GameTime gameTime)
		{
			if (!Main.gameMenu || !TrelamiumClientConfig.Instance.DiscordInvite)
				return;

			base.Update(gameTime);
		}
	}
}
