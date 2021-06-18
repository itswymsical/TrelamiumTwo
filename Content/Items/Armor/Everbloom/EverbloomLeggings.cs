using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Everbloom
{
	[AutoloadEquip(EquipType.Legs)]
	public class EverbloomLeggings : ModItem
	{
		public override string Texture => Assets.Items.Everbloom + "EverbloomLeggings";

		public override void SetDefaults()
		{
			item.defense = 1;

			item.width = 26;
			item.height = 18;

			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(silver: 1, copper: 20);
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 5);
			recipe.AddIngredient(ModContent.ItemType<BloomRose>(), 2);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}