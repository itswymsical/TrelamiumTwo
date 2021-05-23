using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Items;

namespace TrelamiumTwo.Content.Items.Tools.Shovels
{
    public class SilverShovel : ShovelItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Silver Shovel");
        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 6;
            item.useTime = 22;
            item.useAnimation = 22;

            DiggingPower(47);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;

            item.value = Item.sellPrice(silver: 2, copper: 30);
            item.useTurn = true;

            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SilverBar, 8);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}