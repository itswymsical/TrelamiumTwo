using Terraria;
using Terraria.ModLoader;

namespace Trelamium2.Common.Players
{
    public class TrelamiumPlayer : ModPlayer
    {
        public bool mossMonarch;
        public float shakeEffects = 0;
        public override void ResetEffects()
        {
            mossMonarch = false;
            shakeEffects = 0;
        }
        public override void UpdateDead()
        {
            mossMonarch = false;
            shakeEffects = 0;
        }
        public override void UpdateBiomes()
        {
        }
        public override void UpdateBiomeVisuals()
        {

        }
        public override void ModifyScreenPosition()
        {
            if (shakeEffects > 0)
            {
                Main.screenPosition.X += Main.rand.NextFloat(-shakeEffects, shakeEffects + 1);
                Main.screenPosition.Y += Main.rand.NextFloat(-shakeEffects, shakeEffects + 1);
            }
        }
    }
}