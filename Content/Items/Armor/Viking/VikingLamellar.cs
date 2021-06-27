using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Armor.Viking
{
	[AutoloadEquip(EquipType.Body)]
	public class VikingLamellar : ModItem
    {
        public override string Texture => Assets.Items.VikingArmor + "VikingLamellar";
        public override void SetStaticDefaults() => Tooltip.SetDefault("Increases melee damage by 5%");	

		public override void SetDefaults()
		{
            item.value = Item.sellPrice(silver: 2);
            item.rare = ItemRarityID.White;
            item.defense = 4;
		}

        public override void UpdateAccessory(Player player, bool hideVisual) => player.meleeDamage += 0.05f;

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BorealWood, 22);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 4);
            recipe.AddIngredient(ItemID.IceBlock, 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}