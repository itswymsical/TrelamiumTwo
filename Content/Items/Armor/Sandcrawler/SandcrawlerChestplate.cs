using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Sandcrawler
{
    [AutoloadEquip(EquipType.Body)]
    public class SandcrawlerChestplate : ModItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Sandcrawler Chestplate");

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
        }
    }
}
