using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Viking
{
    [AutoloadEquip(EquipType.Body)]
    public class VikingLamellar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Viking Lamellar");
            Tooltip.SetDefault("+4% increased critical strike chance");
        }

        public override void SetDefaults()
        {
            Item.defense = 4;

            Item.width = 34;
            Item.height = 32;

            Item.rare = ItemRarityID.White;
        }

        public override void UpdateEquip(Player player) => player.GetCritChance(DamageClass.Generic) += 4;

        public override void AddRecipes() => CreateRecipe().AddIngredient(ItemID.IceBlock, 20).AddIngredient(ItemID.Leather, 2).AddRecipeGroup("IronBar", 4).Register();
    }
}
