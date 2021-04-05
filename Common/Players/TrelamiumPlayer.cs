using Terraria.ModLoader;

namespace Trelamium2.Common.Players
{
    public class TrelamiumPlayer : ModPlayer
    {
        public bool mossMonarch = false;
        public override void ResetEffects()
        {
            mossMonarch = false;
        }
        public override void UpdateDead()
        {
            mossMonarch = false;
        }
        public override void UpdateBiomes()
        {
        }
        public override void UpdateBiomeVisuals()
        {

        }

    }
}