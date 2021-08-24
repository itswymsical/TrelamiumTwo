using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Sandcrawler
{
    [AutoloadEquip(EquipType.Legs)]
    public class SandcrawlerLeggings : ModItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Sandcrawler Leggings");

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 16;
        }
    }
}
