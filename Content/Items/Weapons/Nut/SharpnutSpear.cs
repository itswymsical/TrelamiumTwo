using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Weapons.Nut
{
	public class SharpnutSpear : ModItem
	{
		public override string Texture => Assets.Weapons.Nut + "SharpnutSpear";
		public override void SetStaticDefaults() => Tooltip.SetDefault("Pierces through enemies and damages on retrieval");
        public override void SetDefaults() 
		{
			item.width = 32;
			item.height = 28;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(copper: 50);
			
			item.damage = 2;
			item.knockBack = 2.5f;
			
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
			recipe.AddIngredient(ModContent.ItemType<Materials.Nut>(), 14);
			recipe.AddIngredient(ModContent.ItemType<Materials.Leaf>(), 8);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
