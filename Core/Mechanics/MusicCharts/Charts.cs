using Terraria;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TrelamiumTwo.Core.Mechanics
{
    public class Charts 
    {
        Mod mod;
        public double tempo;
        public int chartPosition;
        public int arrowType;
        public void DrawChartArrows(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Color? color = null)
        {
            texture = mod.GetTexture("Assets/HarmonicConvergence/Charts_Empty");
            position = new Vector2(Main.screenWidth / 2f, 200f);
            spriteBatch.Draw(texture, position, default);
        }
        public void CollisionWithArrow()
        {

        }

    }
}