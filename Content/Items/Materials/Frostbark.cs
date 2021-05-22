using Terraria.ID;

namespace TrelamiumTwo.Content.Items.Materials
{
    public class Frostbark : TrelamiumItem
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("Frostbark");
        public override void SetDefaults()
        {
            item.rare = ItemRarityID.White;
            item.maxStack = 999;
            item.width = 30;
            item.height = 38;
            item.useAnimation = 12;
            item.useTime = 12;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.value = Terraria.Item.sellPrice(copper: 20);
            //item.createTile = ModContent.TileType<Tiles.CoralstoneTile>();
        }
    }
}