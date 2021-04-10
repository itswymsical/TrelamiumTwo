using Terraria;

namespace TrelamiumTwo.Common.Hooks
{
    public static class TrelamiumHooks
    {
        public static bool IsShovel(this Item item)
        {
            return item.GetGlobalItem<Items.GlobalTrelamiumItem>().Shovel;
        }
        public static bool IsPickaxe(this Item item)
        {
            return item.pick > 0;
        }
    }
}