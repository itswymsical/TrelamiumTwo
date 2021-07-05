using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Weapons.Nut
{
	public class ChestnutLauncher : ModItem
	{
		public override string Texture => Assets.Weapons.Nut + "ChestnutLauncher";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chestnut Launcher");
		}
		public override void SetDefaults()
		{
			item.width = item.height = 20;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(copper: 30);

			item.damage = 4;
			item.knockBack = .5f;

			item.useTime = item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.HoldingOut;
			
			item.ranged = true;
			item.noMelee = true;
			item.useTurn = false;
			item.autoReuse = false;
			
			item.shootSpeed = 7f;
			item.useAmmo = ModContent.ItemType<Materials.Nut>();
			item.shoot = ModContent.ProjectileType<Projectiles.Ranged.NutRocketProjectile>();		
			item.UseSound = SoundID.Item61;
		}
		
		public override Vector2? HoldoutOffset()
			=> new Vector2(-5f, 0f);
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

			return (true);
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 15);
			recipe.AddIngredient(ModContent.ItemType<Materials.Nut>(), 3);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
