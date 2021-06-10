using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.ForestGuardian
{
    public class ForestGuardianTrophy : TrelamiumItem
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("Forest Guardian Trophy");

        public override void SetDefaults()
        {
            item.rare = ItemRarityID.Blue;
            item.maxStack = 999;
            item.width = 18;
            item.height = 24;
            item.useAnimation = 18;
            item.useTime = 18;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;
            item.value = Item.sellPrice(silver: 8);
            item.createTile = ModContent.TileType<Tiles.Furniture.ForestGuardian.ForestGuardianTrophyTile>();
        }
    }
}