using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Weapons.Melee
{
	public class SharpnutSpear : ModItem
	{
		public override void SetDefaults() 
		{
			item.width = 32;
			item.height = 28;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(copper: 50);
			
			item.damage = 5;
			item.knockBack = 3f;
			
			item.useTime = item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			
			item.melee = true;
			item.noMelee = true;
			item.noUseGraphic = true;
			
			item.shootSpeed = 3.5f;
			item.shoot = ModContent.ProjectileType<Projectiles.Melee.SharpnutSpear>();
			
			item.UseSound = SoundID.Item1;
		}

		public override bool CanUseItem(Player player) 
			=> player.ownedProjectileCounts[item.shoot] < 1;

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 16);
			recipe.AddIngredient(ModContent.ItemType<Materials.Nut>(), 4);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
