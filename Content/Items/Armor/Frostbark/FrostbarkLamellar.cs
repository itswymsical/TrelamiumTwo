using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Frostbark
{
	[AutoloadEquip(EquipType.Body)]
	public class FrostbarkLamellar : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Frostbark Lamellar");
            Tooltip.SetDefault("Increases melee damage by 5%");
		}

		public override void SetDefaults()
		{
            item.value = Item.sellPrice(silver: 2);
            item.rare = ItemRarityID.White;
            item.defense = 3;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
            => player.meleeDamage += 0.05f;

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Materials.Frostbark>(), 18);
            recipe.AddIngredient(ItemID.Chain, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}