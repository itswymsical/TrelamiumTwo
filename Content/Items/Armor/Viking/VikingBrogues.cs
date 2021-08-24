using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Viking
{
    [AutoloadEquip(EquipType.Legs)]
    public class VikingBrogues : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Viking Brogues");
            Tooltip.SetDefault("+5% increased melee speed");
        }

        public override void SetDefaults()
        {
            Item.defense = 2;

            Item.width = Item.height = 22;

            Item.rare = ItemRarityID.White;
        }

        public override void UpdateEquip(Player player) => player.meleeSpeed += 0.05f;

        public override void AddRecipes() => CreateRecipe().AddIngredient(ItemID.IceBlock, 15).AddIngredient(ItemID.Leather, 1).AddRecipeGroup("IronBar", 2).Register();
    }
}