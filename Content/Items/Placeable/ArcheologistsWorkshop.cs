using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Placeable
{
    public class ArcheologistsWorkshop : TrelamiumItem
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("Archeologist's Workshop");

        public override void SetDefaults()
        {
            item.rare = ItemRarityID.White;
            item.maxStack = 999;
            item.width = 18;
            item.height = 24;
            item.useAnimation = 18;
            item.useTime = 18;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;
            item.value = Terraria.Item.sellPrice(copper: 80);
            item.createTile = ModContent.TileType<Tiles.Stationary.ArchaeologistsWorkshopTile>();
        }
    }
}