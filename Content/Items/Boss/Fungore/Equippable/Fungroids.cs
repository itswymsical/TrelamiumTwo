using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Players;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Boss.Fungore.Equippable
{
    public class Fungroids : ModItem
    {
        public override string Texture => Assets.Items.Fungore + "Equippable/Fungroids";
        public override void SetStaticDefaults() 
            => Tooltip.SetDefault("Multiplies damage by 12%, in trade reduces health by 12%" + "\nAdditionally, mushrooms heal x3 more health");
        public override void SetDefaults()
        {
            item.width = item.height = 34;
            item.rare = ItemRarityID.Expert;
            item.expert = item.expertOnly = true;
            item.accessory = true;

            item.value = Item.sellPrice(gold: 1, silver: 20);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 -= (int)(player.statLifeMax2 * 0.12f);
            player.allDamage += 0.12f;
            player.GetModPlayer<TrelamiumPlayer>().mushroomHeal = true;
        }
    }
}
