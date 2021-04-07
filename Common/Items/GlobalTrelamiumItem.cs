using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Trelamium2.Common.Hooks;

namespace Trelamium2.Common.Items
{
    public class GlobalTrelamiumItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;

        public bool Shovel;
        public int digPower = 0;
        public static int GetDigPower(int shovel)
        {
            Item i = ModContent.GetModItem(shovel).item;
            return i.GetGlobalItem<GlobalTrelamiumItem>().digPower;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(item, tooltips);
            TooltipLine sline;
            if (item.IsShovel())
            {
                sline = new TooltipLine(mod, "Dig Power", $"{item.GetGlobalItem<GlobalTrelamiumItem>().digPower}% digging power");
                tooltips.Add(sline);
            }
        }
    }
}