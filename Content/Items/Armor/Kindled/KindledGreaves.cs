using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Kindled
{
	[AutoloadEquip(EquipType.Legs)]
	public class KindledGreaves : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Kindled Greaves");
			Tooltip.SetDefault("With each active minion you gain 3% increased movement speed");
		}

		public override void SetDefaults()
		{
			item.value = Item.buyPrice(silver: 1);
			item.rare = ItemRarityID.White;
			item.defense = 1;
		}

		public override void UpdateEquip(Player player) => player.moveSpeed += player.numMinions * .03f;
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 25);
			recipe.AddIngredient(ItemID.Topaz, 2);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}