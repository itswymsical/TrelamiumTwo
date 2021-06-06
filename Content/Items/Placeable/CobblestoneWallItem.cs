using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Placeable
{
    public class CobblestoneWallItem : TrelamiumItem
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("Cobblestone Wall");

        public override void SetDefaults()
        {
            item.rare = ItemRarityID.Blue;
            item.maxStack = 999;

            item.width = 
                item.height = 20;

            item.useAnimation =
                item.useTime = 18;

            item.useStyle = ItemUseStyleID.SwingThrow;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;
            item.value = 0;
            item.createWall = ModContent.WallType<Walls.CobblestoneWall>();
        }
    }
}