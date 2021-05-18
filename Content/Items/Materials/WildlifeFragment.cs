#region Using directives

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Items.Materials
{
	public sealed class WildlifeFragment : ModItem
	{
		public override void SetDefaults()
		{
			item.width = item.height = 20;
			item.maxStack = 999;
			item.value = Item.sellPrice(silver: 5);
			item.rare = ItemRarityID.Blue;

			item.material = true;
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 3);
			recipe.AddIngredient(ModContent.ItemType<Nut>(), 2);
			recipe.AddIngredient(ModContent.ItemType<Leaf>(), 2);
			recipe.AddIngredient(ModContent.ItemType<AncientTwig>(), 3);
			recipe.AddTile(TileID.LivingLoom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
