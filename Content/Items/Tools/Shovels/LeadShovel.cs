using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Trelamium2.Common.Items;

namespace Trelamium2.Content.Items.Tools.Shovels
{
    public class LeadShovel : ShovelItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Lead Shovel");
        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 6;
            item.useTime = 25;
            item.useAnimation = 25;

            diggingPower(45);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;

            item.value = Item.sellPrice(silver: 1, copper: 45);
            item.useTurn = true;

            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LeadBar, 8);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}