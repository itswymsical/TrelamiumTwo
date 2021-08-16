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
			Item.width = 32;
			Item.height = 28;
			Item.rare = ItemRarityID.White;
			Item.value = Item.sellPrice(copper: 50);
			
			Item.damage = 2;
			Item.knockBack = 2.5f;
			
			Item.useTime = Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			
			Item.DamageType = DamageClass.Melee;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			
			Item.shootSpeed = 3.5f;
			Item.shoot = ModContent.ProjectileType<Projectiles.Melee.SharpnutSpear>();
			
			Item.UseSound = SoundID.Item1;
		}

		public override bool CanUseItem(Player player) 
			=> player.ownedProjectileCounts[Item.shoot] < 1;

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Materials.Nut>(), 14).AddIngredient(ModContent.ItemType<Materials.Leaf>(), 8).AddTile(TileID.WorkBenches).Register();
		}
	}
}
