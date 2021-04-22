using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Players;

namespace TrelamiumTwo.Content.Items.Armor.Coralstone
{
	[AutoloadEquip(EquipType.Legs)]
	public class CoralstoneGreaves : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coralstone Greaves");
			Tooltip.SetDefault("Increases melee damage by 5%");
		}

		public override void SetDefaults()
		{
			item.value = Item.sellPrice(copper: 75);
			item.rare = ItemRarityID.Blue;
			item.defense = 2;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.05f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Materials.Coralstone>(), 12);
			recipe.AddIngredient(ItemID.Seashell, 4);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}