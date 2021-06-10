using Terraria;
using TrelamiumTwo.Core.Mechanics.Trails;

namespace TrelamiumTwo.Core.Detours
{
    public class TrailDetours : Detour
    {
        public override void LoadDetours() => On.Terraria.Main.DrawInfernoRings += DrawTrails;

        public override void UnloadDetours() => On.Terraria.Main.DrawInfernoRings -= DrawTrails;

        private void DrawTrails(On.Terraria.Main.orig_DrawInfernoRings orig, Main self)
        {
            if (!Main.dedServ)
            {
                TrailManager.Instance.DrawTrails();
            }

            orig(self);
        }
    }
}
