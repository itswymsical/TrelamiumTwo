using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Tiles
{
    public class AlderwoodTree : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "Terraria/Projectile_" + ProjectileID.None;
            return base.Autoload(ref name, ref texture);
        }
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileAxe[Type] = true;
            
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Texture2D texture = mod.GetTexture("Assets/AlderTree_Texture");
            var drawPosition = ((new Vector2(i, j) * 16) - new Vector2(0, 23f) * 16) - Main.screenPosition;
            spriteBatch.Draw(texture, drawPosition, null, Color.White, 0f, default, 1f, SpriteEffects.None, 1f);
            return false;
        }
    }
}