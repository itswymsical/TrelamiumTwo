using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace TrelamiumTwo.Common.Items
{
    public abstract class FoodItem : ModItem
    {
        public override bool CloneNewInstances => true;
        public int ExpireTimer;
        public bool expired = false;
        public bool canExpire = true;
        public override TagCompound Save()
        {
            return new TagCompound
            {
                [nameof(ExpireTimer)] = ExpireTimer,
            };
        }

        public override void Load(TagCompound tag)
            => ExpireTimer = tag.GetInt(nameof(ExpireTimer));

        public override void NetSend(BinaryWriter writer)
            => writer.Write(ExpireTimer);

        public override void NetRecieve(BinaryReader reader)
            => ExpireTimer = reader.ReadInt32();

        public override void UpdateInventory(Player player)
        {
            ExpireTimer -= 1;
            if (ExpireTimer <= 0)
            {
                ExpireTimer = 0;
                expired = true;
            }
        }

        public override void PostUpdate()
        {
            ExpireTimer -= 1;
            if (ExpireTimer <= 0)
            {
                ExpireTimer = 0;
                expired = true;
            }
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string seconds = "seconds";
            string minutes = "minutes";
            string hours = "hours";

            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "BuffTime" && x.mod == "Terraria");
            if (tt != null)
            {
                tt.text = "Has lasting status effects when consumed";
                tt.overrideColor = Color.LightYellow;
            }
            TooltipLine tt2 = tooltips.FirstOrDefault(x => x.Name == "HealLife" && x.mod == "Terraria");
            if (tt2 != null)
            {
                tt2.text = "Has lasting status effects when consumed";
                tt2.overrideColor = Color.LightYellow;
            }
            TooltipLine tt3 = tooltips.FirstOrDefault(x => x.Name == "HealMana" && x.mod == "Terraria");
            if (tt3 != null)
            {
                tt3.text = "Has lasting status effects when consumed";
                tt3.overrideColor = Color.LightYellow;
            }
            if (canExpire && item.consumable && ExpireTimer < 3600 && !expired)
            {
                TooltipLine sline2 =
                    new TooltipLine(mod, "TrelamiumTwo: Expiry (seconds)", $"Expires in: [c/FA8072:{ExpireTimer / 60}] [c/FA8072:{seconds:N0}]");
                tooltips.Add(sline2);
            }
            if (canExpire && item.consumable && ExpireTimer > 3599 && ExpireTimer < 216000 && !expired)
            {
                TooltipLine sline2 =
                    new TooltipLine(mod, "TrelamiumTwo: Expiry (minutes)", $"Expires in: [c/FA8072:{ExpireTimer / 3599}] [c/FA8072:{minutes:N0}]");
                tooltips.Add(sline2);
            }
            if (canExpire && item.consumable && ExpireTimer > 215999 && !expired)
            {
                TooltipLine sline2 =
                    new TooltipLine(mod, "TrelamiumTwo: Expiry (hours)", $"Expires in: [c/FA8072:{ExpireTimer / 216000}] [c/FA8072:{hours:N0}]");
                tooltips.Add(sline2);
            }
            if (canExpire && item.consumable && expired)
            {
                TooltipLine sline2 =
                    new TooltipLine(mod, "TrelamiumTwo: Expired", "[c/FA8072:Expired food]");
                tooltips.Add(sline2);
            }
        }
    }
}