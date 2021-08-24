using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Players;

namespace TrelamiumTwo.Content.Items.Armor.Viking
{
    [AutoloadEquip(EquipType.Head)]
    public class VikingHelmet : ModItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Viking Helmet");

        public override void SetDefaults()
        {
            Item.defense = 1;

            Item.width = 38;
            Item.height = 22;

            Item.rare = ItemRarityID.White;
        }

        public override void UpdateArmorSet(Player player) => player.GetModPlayer<ArmorSetPlayer>().VikingSet = true;

        public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<VikingLamellar>() && legs.type == ModContent.ItemType<VikingBrogues>();

        public override void AddRecipes() => CreateRecipe().AddIngredient(ItemID.IceBlock, 10).AddIngredient(ItemID.Leather, 1).AddRecipeGroup("IronBar", 2).Register();
    }
}