using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Core;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Everbloom
{
	[AutoloadEquip(EquipType.Legs)]
	public class BlossomLeggings : ModItem
	{
		public override string Texture => Assets.Armors.BloomRose + "BlossomLeggings";

		public override void SetDefaults()
		{
			item.defense = 1;

			item.width = 26;
			item.height = 18;

			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 1, copper: 20);
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<BloomRose>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Leaf>(), 4);
			recipe.AddIngredient(ItemID.Wood, 15);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}