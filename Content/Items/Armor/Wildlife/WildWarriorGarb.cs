using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Armor.WildWarrior
{
	[AutoloadEquip(EquipType.Body)]
	public class WildWarriorGarb : ModItem
	{
		public override string Texture => Assets.Armor.Wildlife + "WildWarriorGarb";
		public override void SetStaticDefaults() => DisplayName.SetDefault("Wild Warrior Garb");

		public override void SetDefaults()
		{
			item.defense = 3;

			item.width = 38;
			item.height = 22;

			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 8);
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<WildlifeFragment>(), 10);
			recipe.AddTile(TileID.LivingLoom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}