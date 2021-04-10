using Terraria.ModLoader;

namespace TrelamiumTwo.Core
{
    internal static class WeakReferences
    {
        public static void PerformModSupport()
        {
            PerformBossChecklistSupport();
            PerformYABHBSupport();
        }

        private static void PerformBossChecklistSupport()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {

            }
        }

        private static void PerformYABHBSupport()
        {
            Mod yabhb = ModLoader.GetMod("FKBossHealthBar");
            if (yabhb != null)
            {

            }
        }
    }
}
