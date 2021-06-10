using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TrelamiumTwo.Core.Mechanics.Particles;

namespace TrelamiumTwo.Core.Detours
{
    public class ParticleDetours : Detour
    {
        public override void LoadDetours() => On.Terraria.Main.DrawDust += DrawParticles;

        public override void UnloadDetours() => On.Terraria.Main.DrawDust -= DrawParticles;

        private void DrawParticles(On.Terraria.Main.orig_DrawDust orig, Main self)
        {
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);

            if (!Main.dedServ)
            {
                ParticleManager.Instance.DrawParticles(Main.spriteBatch);
            }

            Main.spriteBatch.End();

            orig(self);
        }
    }
}
