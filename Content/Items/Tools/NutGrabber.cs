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
			Item.CloneDefaults(ItemID.AmethystHook);

			Item.rare = ItemRarityID.Blue;
			Item.shootSpeed = 12f;
			Item.shoot = ModContent.ProjectileType<Projectiles.Typeless.NutGrabberProjectile>();
		}
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Materials.Leaf>(), 4).AddIngredient(ModContent.ItemType<Materials.Nut>(), 8).AddTile(TileID.Anvils).Register();
		}
	}
}