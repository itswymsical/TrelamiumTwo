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
			Item.defense = 3;

			Item.width = 38;
			Item.height = 22;

			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 8);
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<WildlifeFragment>(), 10).AddTile(TileID.LivingLoom).Register();
		}
	}
}