using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Materials
{
	public class WildlifeFragment : ModItem
	{
		public override string Texture => Assets.Items.Materials + "WildlifeFragment";
		public override void SetDefaults()
		{
			item.width = item.height = 20;
			item.maxStack = 999;
			item.value = Item.sellPrice(copper: 25);
			item.rare = ItemRarityID.Blue;
		}
        public override void AddRecipes()
        {
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Nut>(), 3);
			recipe.AddIngredient(ModContent.ItemType<Leaf>(), 2);
			recipe.AddIngredient(ModContent.ItemType<Alderwood>(), 2);
		}
    }
}
