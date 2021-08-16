using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Core;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.BloomRose
{
	[AutoloadEquip(EquipType.Legs)]
	public class BlossomLeggings : ModItem
	{
		public override string Texture => Assets.Armor.BloomRose + "BlossomLeggings";

		public override void SetDefaults()
		{
			Item.defense = 1;

			Item.width = 26;
			Item.height = 18;

			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 1, copper: 20);
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Materials.BloomRose>(), 1).AddIngredient(ModContent.ItemType<Leaf>(), 4).AddIngredient(ItemID.Wood, 15).AddTile(TileID.Anvils).Register();
		}
	}
}