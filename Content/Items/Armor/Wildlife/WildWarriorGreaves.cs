using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Armor.WildWarrior
{
	[AutoloadEquip(EquipType.Legs)]
	public class WildWarriorGreaves : ModItem
	{
		public override string Texture => Assets.Armor.Wildlife + "WildWarriorGreaves";
		public override void SetStaticDefaults() => DisplayName.SetDefault("Wild Warrior Greaves");

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 16;

			Item.defense = 1;

			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 5);
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<WildlifeFragment>(), 5).AddTile(TileID.LivingLoom).Register();
		}
	}
}
