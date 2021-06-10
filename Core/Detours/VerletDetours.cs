using Terraria;
using TrelamiumTwo.Core.Mechanics.Verlet;

namespace TrelamiumTwo.Core.Detours
{
    public class VerletDetours : Detour
    {
        public override void LoadDetours() => On.Terraria.Main.DrawInfernoRings += DrawVerlet;

        public override void UnloadDetours() => On.Terraria.Main.DrawInfernoRings -= DrawVerlet;

        private void DrawVerlet(On.Terraria.Main.orig_DrawInfernoRings orig, Main self)
        {
            if (!Main.dedServ)
            {
                VerletManager.Instance.DrawChains(Main.spriteBatch);
            }

            orig(self);
        }
    }
}
