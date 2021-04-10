using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Placeable
{
    public class StellarConduitPlating : TrelamiumItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Stellar Conduit Plating");
        public override void SetDefaults()
        {
            item.useAnimation = 20;
            item.useTime = 20;
            item.maxStack = 999;

            item.autoReuse = true;
            item.consumable = true;

            item.rare = ItemRarityID.Cyan;
            item.value = Item.buyPrice(silver: 25);
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.createTile = ModContent.TileType<Tiles.StellarConduitPlatingTile>();
        }
    }
}