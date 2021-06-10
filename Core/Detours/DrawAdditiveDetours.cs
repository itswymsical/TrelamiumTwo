using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TrelamiumTwo.Core.Abstraction.Interfaces;

namespace TrelamiumTwo.Core.Detours
{
    public class DrawAdditiveDetours : Detour
    {
        public override void LoadDetours() => On.Terraria.Main.DrawDust += DrawAdditive;

        public override void UnloadDetours() => On.Terraria.Main.DrawDust -= DrawAdditive;

        private void DrawAdditive(On.Terraria.Main.orig_DrawDust orig, Main self)
        {
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);

            foreach (var projectile in Main.projectile)
            {
                var modProjectile = projectile.modProjectile;

                if (modProjectile is IDrawAdditive && projectile.active)
                {
                    (modProjectile as IDrawAdditive).DrawAdditive(Main.spriteBatch);
                }
            }

            foreach (var npc in Main.npc)
            {
                var modNPC = npc.modNPC;

                if (modNPC is IDrawAdditive && npc.active)
                {
                    (modNPC as IDrawAdditive).DrawAdditive(Main.spriteBatch);
                }
            }

            Main.spriteBatch.End();

            orig(self);
        }
    }
}
