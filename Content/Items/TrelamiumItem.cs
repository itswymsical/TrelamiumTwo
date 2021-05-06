using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items
{
    public abstract class TrelamiumItem : ModItem
    {
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
            SafeSetDefaults();
        }

        public virtual void SafeSetDefaults() { }
    }
}
