using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

using TrelamiumTwo.Common.Globals;
using TrelamiumTwo.Helpers;

namespace TrelamiumTwo.Common.Items
{
    public abstract class ShovelItem : ModItem
    {
        public override bool CloneNewInstances => false;
        public void DiggingPower(int digPower)
        {
            Item.IsShovel();
            Item.GetGlobalItem<GlobalTrelamiumItem>().digPower = digPower;
            Item.GetGlobalItem<GlobalTrelamiumItem>().radius = 6;
            return;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tooltipLine;
            tooltipLine = new TooltipLine(Mod, "T2:Digging Power", Item.GetGlobalItem<GlobalTrelamiumItem>().digPower + "% digging power");
            tooltips.Add(tooltipLine);
        }
        
        public static int GetDigPower(int shovel)
        {
            Item i = ModContent.GetModItem(shovel).Item;
            return i.GetGlobalItem<GlobalTrelamiumItem>().digPower;
        }
        public static int GetShovelRadius(int shovel)
        {
            Item i = ModContent.GetModItem(shovel).Item;
            return i.GetGlobalItem<GlobalTrelamiumItem>().radius;
        }
        public void DigTile(Player player, int rangeinBlocks)
        {
            if (player.Distance(Main.MouseWorld) < 16 * rangeinBlocks)
            {
                player.GetModPlayer<Players.TrelamiumPlayer>().ShovelPickTile((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y);
            }
        }
        public override bool? UseItem(Player player)
        {
            DigTile(player, 5);
            return true;
        }
        public override int ChoosePrefix(UnifiedRandom rand) => rand.Next(new int[]
        {
            PrefixID.Agile,
            PrefixID.Quick,
            PrefixID.Light,

            PrefixID.Slow,
            PrefixID.Sluggish,
            PrefixID.Lazy,

            PrefixID.Large,
            PrefixID.Tiny,

            PrefixID.Bulky,
            PrefixID.Heavy,

            PrefixID.Damaged,
            PrefixID.Broken,

            PrefixID.Unhappy,
            PrefixID.Nimble,
            PrefixID.Dull,
            PrefixID.Awkward
        });

    }
}