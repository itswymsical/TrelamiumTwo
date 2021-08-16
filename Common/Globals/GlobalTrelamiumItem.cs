using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Items;
using TrelamiumTwo.Common.Players;
using TrelamiumTwo.Helpers;

namespace TrelamiumTwo.Common.Globals
{
    public class GlobalTrelamiumItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;

        public bool Shovel;
        public int digPower;
        public int radius = 2;
        public override void ExtractinatorUse(int extractType, ref int resultType, ref int resultStack)
        {
            if (extractType == ItemID.DesertFossil)
            {
                if (Main.rand.NextFloat() < 0.005f)
                {
                    //resultType = ModContent.ItemType<Microlith>();
                }
            }
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
        public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
        {
            ArmorSetPlayer armorSetPlayer = Main.LocalPlayer.GetModPlayer<ArmorSetPlayer>();
            if (armorSetPlayer.vikingSet && item.melee)
            {
                if (Main.rand.Next(12) == 0)
                    target.AddBuff(BuffID.Frostburn, Main.rand.Next(60, 140));
            }
        }
    }
}