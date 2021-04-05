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
                if (ModContent.TextureExists(base.Texture))
                    return base.Texture;

                return Trelamium2.PLACEHOLDER_TEXTURE;
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
