#region Using directives

using System.Linq;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TrelamiumTwo.Common.Players;

#endregion

namespace TrelamiumTwo.Content.Items.Tools
{
	public sealed class TravelersLantern : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Traveler's Lantern");
		}
		public override void SetDefaults()
		{
			item.width = item.height = 12;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(0, 0, 40, 0);

			item.holdStyle = ItemHoldStyleID.HoldingOut;

			item.noMelee = true;
		}
        public override void HoldItem(Player player)
        {
			Lighting.AddLight(player.itemLocation, Color.Orange.ToVector3() * Main.essScale);
        }
        public override void HoldStyle(Player player)
		{
			player.itemLocation.Y -= 22;
			if (player.direction == 1)
			{
				player.itemLocation.X -= 48;
			}
			player.itemLocation.X -= 6 * player.direction;

			Lighting.AddLight(player.itemLocation, new Vector3(0.5f, 0.2f, 0f) * 0.5f);

			player.GetModPlayer<TrelamiumPlayer>().HeldItemOverlayOperationModifier += TravelersLanternGlow;
		}

		private void TravelersLanternGlow(Player player, TrelamiumPlayer mPlayer)
		{
			Texture2D glowmask = ModContent.GetTexture(Texture + "_Glow");
			Rectangle frame = glowmask.Frame();
			SpriteEffects effects = player.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			Vector2 origin = new Vector2(0, 0);
			
			Vector2 drawPosition = player.itemLocation - Main.screenPosition + new Vector2(0, player.gfxOffY);
			drawPosition.X += 33 * player.direction;

			drawPosition.X = (int)drawPosition.X;
			drawPosition.Y = (int)drawPosition.Y;

			float opacity = 0.4f + System.Math.Abs((float)System.Math.Sin(Main.time / 30));
			if (opacity > 1f)
			{
				opacity = 1f;
			}
			DrawData glowmaskData = new DrawData(glowmask, drawPosition, frame, Color.White * opacity, 0, origin, item.scale, effects, 0);
			Main.playerDrawData.Add(glowmaskData);
		}
	}
}
