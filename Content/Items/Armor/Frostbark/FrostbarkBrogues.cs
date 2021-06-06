using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Frostbark
{
	[AutoloadEquip(EquipType.Legs)]
	public class FrostbarkBrogues : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Frostbark Brogues");
            Tooltip.SetDefault("Increases melee speed by 4%");
		}

		public override void SetDefaults()
		{
            item.value = Item.sellPrice(silver: 2);
            item.rare = ItemRarityID.White;
            item.defense = 1;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
            => player.meleeSpeed += 0.04f;

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BorealWood, 14);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 3);
            recipe.AddIngredient(ItemID.Chain, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}