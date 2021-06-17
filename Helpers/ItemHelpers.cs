using Terraria;
using Terraria.ModLoader;

using TrelamiumTwo.Common.Globals;

namespace TrelamiumTwo.Helpers
{
    internal static partial class Helper
    {
        public static bool IsShovel(this Item item) => item.GetGlobalItem<GlobalTrelamiumItem>().Shovel;
        public static bool IsPickaxe(this Item item) => item.pick > 0;
        
    }
}