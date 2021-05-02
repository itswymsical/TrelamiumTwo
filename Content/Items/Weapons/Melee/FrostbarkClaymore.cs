using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Content.Items.Materials;

namespace TrelamiumTwo.Content.Items.Weapons.Melee
{
    public class FrostbarkClaymore : TrelamiumItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Frostbark Claymore");

        public override void SafeSetDefaults()
        {
            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.useAnimation = 28;
            item.useTime = 28;
            item.damage = 11;
            item.crit = 7;

            item.knockBack = 4.5f;

            item.rare = ItemRarityID.White;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;

            item.value = Item.buyPrice(silver: 50);
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Frostbark>(), 18);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 2);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}