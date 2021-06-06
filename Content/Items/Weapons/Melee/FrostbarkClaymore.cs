using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Weapons.Melee
{
    public class FrostbarkClaymore : TrelamiumItem
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("Frostbark Claymore");

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
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 30);
        }
        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BorealWood, 18);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 2);
            recipe.AddIngredient(ItemID.IceBlock, 6);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}