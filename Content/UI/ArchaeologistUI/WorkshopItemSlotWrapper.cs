using System;

using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.GameInput;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrelamiumTwo.Content.UI.ArchaeologistUI
{
    internal sealed class WorkshopItemSlotWrapper : UIElement
	{		
		private readonly int _context;
		private readonly float _scale;

		internal Item Item;
		internal Func<Item, bool> ValidItemFunc;

		public WorkshopItemSlotWrapper(int context = ItemSlot.Context.BankItem, float scale = 1f)
		{
			_context = context;
			_scale = scale;

			Item = new Item();
			Item.SetDefaults(0);

			Width.Set(Main.inventoryBack9Texture.Width * scale, 0f);
			Height.Set(Main.inventoryBack9Texture.Height * scale, 0f);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			float oldScale = Main.inventoryScale;
			Main.inventoryScale = _scale;

			Rectangle rectangle = GetDimensions().ToRectangle();
			
			if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
			{
				Main.LocalPlayer.mouseInterface = true;

				if (ValidItemFunc == null || ValidItemFunc(Main.mouseItem))
				{
					ItemSlot.Handle(ref Item, _context);
				}
			}

			Texture2D ItemTexture = Main.itemTexture[Item.type];
			spriteBatch.Draw(ModContent.GetTexture("TrelamiumTwo/Assets/UI/WorkshopSlot"), rectangle.TopLeft(), Color.White);
			spriteBatch.Draw(ItemTexture, rectangle.Center() - ItemTexture.Size() / 2, Color.White);

			Main.inventoryScale = oldScale;
		}
    }
}
