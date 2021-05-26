using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Items;

namespace TrelamiumTwo.Content.Items.Tools.Shovels
{
    public class CopperShovel : ShovelItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Copper Shovel");
        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 4;
            item.useTime = 22;
            item.useAnimation = 22;

            DiggingPower(37);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;

            item.value = Item.sellPrice(copper: 75);
            item.useTurn = true;

            item.tileBoost--;
            item.UseSound = SoundID.Item18;
        }
        public override bool UseItem(Player player)
        {
            DigTile(player, 4);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CopperBar, 8);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}