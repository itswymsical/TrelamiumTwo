using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TrelamiumTwo.Content.Items.Materials
{
    public sealed class Coralstone : TrelamiumItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Coralstone");
        public override void SetDefaults()
        {
            item.rare = ItemRarityID.Blue;
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
            item.createTile = TileType<Tiles.CoralstoneTile>();
        }
    }
}