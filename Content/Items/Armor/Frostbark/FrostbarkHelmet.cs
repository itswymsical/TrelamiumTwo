using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Players;

namespace TrelamiumTwo.Content.Items.Armor.Frostbark
{
	[AutoloadEquip(EquipType.Head)]
	public class FrostbarkHelmet : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Frostbark Helmet");
            Tooltip.SetDefault("Increases melee critical strike chance by 5%");
		}

		public override void SetDefaults()
		{
            item.value = Item.sellPrice(silver: 2);
            item.rare = ItemRarityID.White;
            item.defense = 2;
		} 

        public override bool IsArmorSet(Item head, Item body, Item legs)
            => body.type == ModContent.ItemType<FrostbarkLamellar>() && legs.type == ModContent.ItemType<FrostbarkBrogues>();

        public override void UpdateAccessory(Player player, bool hideVisual) => player.meleeCrit += 5;

        public override void UpdateArmorSet(Player player)
        {
            ArmorSetPlayer armorSetPlayer = player.GetModPlayer<ArmorSetPlayer>();
            player.setBonus = "Melee weapons are imbued with 'Frostburn'" +
                "\nWhile in the tundra you deal more 'Frostburn' damage";
            armorSetPlayer.frostbarkSet = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BorealWood, 16);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 2);
            recipe.AddIngredient(ItemID.Chain, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}