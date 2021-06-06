using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Content.Tiles.DruidsGarden;

namespace TrelamiumTwo.Content.Items.Placeable
{
    public class LoamGrassSeeds : TrelamiumItem
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("Loam Grass Seeds");

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
            item.value = 0;
        }
        public override bool UseItem(Player player)
        {
            if (Main.netMode == NetmodeID.Server)
                return false;

            Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
            if (tile.active() && tile.type == TileID.Dirt && player.WithinRange(new Microsoft.Xna.Framework.Vector2(Player.tileTargetX, Player.tileTargetY), default))
            {
                WorldGen.PlaceTile(Player.tileTargetX, Player.tileTargetY, ModContent.TileType<LoamTileGrass>(), forced: true);
                player.inventory[player.selectedItem].stack--;
            }

            return true;
        }
    }
}