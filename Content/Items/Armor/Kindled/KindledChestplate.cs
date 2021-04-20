#region Directives
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
#endregion

namespace TrelamiumTwo.Content.Items.Armor.Kindled
{
	[AutoloadEquip(EquipType.Body)]
	public class KindledChestplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Kindled Chestplate");
			Tooltip.SetDefault("Increases minion slots by 1");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.buyPrice(silver: 15);
			item.rare = ItemRarityID.White;
			item.defense = 2;
		}

		public override void UpdateEquip(Player player) => player.maxMinions++;
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 30);
			recipe.AddIngredient(ItemID.Topaz, 4);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}