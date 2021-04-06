using Terraria.ModLoader;
using Trelamium2.Common.Utilities;

namespace Trelamium2.Content.Items
{
    public abstract class TrelamiumItem : ModItem
    {
        public bool Autosize;

        public override string Texture
        {
            get
            {
                mod.Logger.Warn("Couldn't find a texture for " + Name + ", going for a placeholder texture!");

                return ModContent.TextureExists(base.Texture) ? base.Texture : Trelamium2.PLACEHOLDER_TEXTURE;
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
