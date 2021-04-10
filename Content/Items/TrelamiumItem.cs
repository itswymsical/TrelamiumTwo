using Terraria.ModLoader;
using TrelamiumTwo.Common.Utilities;

namespace TrelamiumTwo.Content.Items
{
    public abstract class TrelamiumItem : ModItem
    {
        public bool Autosize;
        public override string Texture
        {
            get
            {
                mod.Logger.Warn("Couldn't find a texture for " + Name + ", going for a placeholder texture!");

                return ModContent.TextureExists(base.Texture) ? base.Texture : TrelamiumTwo.PLACEHOLDER_TEXTURE;
            }
        }

        public override void SetDefaults()
        {
            if (Autosize)
                item.Autosize();

            SafeSetDefaults();
        }

        public virtual void SafeSetDefaults() { }
    }
}
