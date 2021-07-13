using TrelamiumTwo.Content.Items.Materials;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.WildWarrior
{
	[AutoloadEquip(EquipType.Legs)]
	public class WildWarriorGreaves : ModItem
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Wild Warrior Greaves");

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 16;

			item.defense = 1;

			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 5);
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<WildlifeFragment>(), 5);
			recipe.AddTile(TileID.LivingLoom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
