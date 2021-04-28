#region Using Directives
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Hooks;
#endregion

namespace TrelamiumTwo.Common.Items
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
        public override void UpdateInventory(Item item, Player player)
        {
            if (item.Name.Contains("Boots") || player.moveSpeed >= 0)
                item.shoeSlot = 0;
        }
        public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Players.TrelamiumPlayer tp = Main.LocalPlayer.GetModPlayer<Players.TrelamiumPlayer>();
            if (tp.frostbarkBonus && item.melee)
            {
                if (Main.rand.Next(12) == 0)
                    target.AddBuff(BuffID.Frostburn, Main.rand.Next(60, 140));
            }          
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            Player player = Main.LocalPlayer;
            base.ModifyTooltips(item, tooltips);
            TooltipLine sline;
            if (item.IsShovel())
            {
                sline = new TooltipLine(mod, "TrelamiumTwo:Digging Power", $"{item.GetGlobalItem<GlobalTrelamiumItem>().digPower}% digging power");
                tooltips.Add(sline);
            }
            if (item.shoeSlot >= 0 && player.moveSpeed > 0)
            {
                sline = new TooltipLine(mod, "TrelamiumTwo:Movement Speed", $"{player.moveSpeed}x movement speed");
                sline.overrideColor = Color.LightYellow;
                tooltips.Add(sline);
            }
        }
    }
}