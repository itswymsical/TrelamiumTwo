using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Tools
{
	public class NutGrabber : ModItem
	{
		public override string Texture => Assets.Items.Tools + "NutGrabber";
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.AmethystHook);

			item.rare = ItemRarityID.Blue;
			item.shootSpeed = 12f;
			item.shoot = ModContent.ProjectileType<Projectiles.Typeless.NutGrabberProjectile>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Materials.Leaf>(), 4);
			recipe.AddIngredient(ModContent.ItemType<Materials.Nut>(), 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}