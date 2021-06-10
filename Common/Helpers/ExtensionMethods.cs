using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace TrelamiumTwo.Common.Helpers
{
    internal static class ExtensionMethods
    {
        /// <summary>
        /// Automatically sets an item's size.
        /// </summary>
        public static Vector2 Autosize(this Item item)
        {
            Texture2D texture = Main.itemTexture[item.type];

            if (texture == null)
                return Vector2.Zero;

            return item.Size = Main.itemAnimationsRegistered.Contains(item.type)
                ? new Vector2(texture.Width, texture.Height / Main.itemAnimations[item.type].FrameCount)
                : texture.Size();
        }
    }
}
