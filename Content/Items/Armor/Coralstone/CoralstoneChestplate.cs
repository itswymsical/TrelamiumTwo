using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Coralstone
{
	[AutoloadEquip(EquipType.Body)]
	public class CoralstoneChestplate : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coralstone Chestplate");
			Tooltip.SetDefault("Increases damage reduction by 5%");
		}

		public override void SetDefaults()
		{
			item.value = Item.sellPrice(silver: 1);
			item.rare = ItemRarityID.Blue;
			item.defense = 4;
		}

		public override void UpdateEquip(Player player)
		{
			player.endurance += .05f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Materials.Coralstone>(), 14);
			recipe.AddIngredient(ItemID.Seashell, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}