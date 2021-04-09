using Terraria.ID;
using Terraria.ModLoader;

namespace Trelamium2.Content.Items.Placeable
{
    public class AlderwoodTreePlacer : TrelamiumItem
    {
        public override void SetDefaults()
        {
            item.rare = ItemRarityID.Yellow;
            item.maxStack = 999;
            item.width = 18;
            item.height = 24;
            item.useAnimation = 18;
            item.useTime = 18;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.autoReuse = true;
            item.createTile = ModContent.TileType<Tiles.AlderwoodTree>();
        }
    }
}