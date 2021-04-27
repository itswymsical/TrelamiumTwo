#region Using directives

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Items.Ammo
{
	public sealed class DustiliteArrow : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 10;
			item.height = 28;
			item.value = Item.buyPrice(0, 0, 0, 15);
			item.maxStack = 999;
			
			item.damage = 9;
			item.knockBack = 2f;

			item.ranged = true;
			item.consumable = true;

			item.shootSpeed = 4f;
			item.ammo = AmmoID.Arrow;
			item.shoot = ModContent.ProjectileType<Projectiles.Ranged.DustiliteArrow>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.WoodenArrow, 100);
			recipe.AddIngredient(ModContent.ItemType<Materials.DustiliteCrystal>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}