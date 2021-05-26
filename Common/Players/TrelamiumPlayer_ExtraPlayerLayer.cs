using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Common.Players
{
	public delegate void HeldItemOverlayOperation(Player player, TrelamiumPlayer modPlayer);

	public partial class TrelamiumPlayer : ModPlayer
	{
		public HeldItemOverlayOperation HeldItemOverlayOperationModifier;

		private void ResetEffects_ExtraPlayerLayer()
		{
			HeldItemOverlayOperationModifier = null;
		}

		private void ModifyDrawLayers_ExtraPlayerLayer(List<PlayerLayer> layers)
		{
			HeldItemOverlayLayer.visible = true;

			int heldItemLayerIndex = layers.FindIndex(x => x.Name == "HeldItem");
			layers.Insert(heldItemLayerIndex + 1, HeldItemOverlayLayer);
		}

		public static readonly PlayerLayer HeldItemOverlayLayer = new PlayerLayer("TrelamiumTwo", "HeldItemOverlayLayer", PlayerLayer.HeldItem, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f)
			{
				return;
			}

			Player drawPlayer = drawInfo.drawPlayer;
			TrelamiumPlayer modPlayer = drawPlayer.GetModPlayer<TrelamiumPlayer>();

			try
			{
				modPlayer.HeldItemOverlayOperationModifier?.Invoke(drawPlayer, modPlayer);
			}
			catch (System.Exception ex)
			{
				Main.NewText(ex.Message, Color.Red);
			}
		});
	}
}
