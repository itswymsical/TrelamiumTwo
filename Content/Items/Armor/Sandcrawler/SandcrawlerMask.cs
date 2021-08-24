using Terraria;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Players;

namespace TrelamiumTwo.Content.Items.Armor.Sandcrawler
{
    [AutoloadEquip(EquipType.Head)]
    public class SandcrawlerMask : ModItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("SandcrawlerMask");

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 24;
        }

        public override void UpdateArmorSet(Player player) => player.GetModPlayer<ArmorSetPlayer>().SandcrawlerSet = true;

        public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<SandcrawlerChestplate>() && legs.type == ModContent.ItemType<SandcrawlerLeggings>();
    }
}
