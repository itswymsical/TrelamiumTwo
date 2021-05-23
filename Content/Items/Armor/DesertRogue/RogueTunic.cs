#region Using Directives
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
#endregion

namespace TrelamiumTwo.Content.Items.Armor.DesertRogue
{
	[AutoloadEquip(EquipType.Body)]
	public class RogueTunic : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Raider Tunic");
			Tooltip.SetDefault("Increases ranged damage by 5%");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 1, copper: 50);
			item.rare = ItemRarityID.White;
			item.defense = 3;
		}

		public override void UpdateEquip(Player player)
		{
			player.rangedDamage += 0.05f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Leather, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}