using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Items;

namespace TrelamiumTwo.Content.Items.Tools.Shovels
{
    public class CactusShovel : ShovelItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Cactus Shovel");
        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 4;
            item.useTime = 16;
            item.useAnimation = 16;

            DiggingPower(37);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;

            item.value = Item.sellPrice(copper: 60);
            item.useTurn = true;

            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cactus, 20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}