using Terraria.ModLoader;

namespace TrelamiumTwo.Common.Players
{
    public class ArmorSetPlayer : ModPlayer
    {
        public bool VikingSet;
        public bool SandcrawlerSet;

        public override void ResetEffects()
        {
            VikingSet = false;
            SandcrawlerSet = false;
        }
    }
}
