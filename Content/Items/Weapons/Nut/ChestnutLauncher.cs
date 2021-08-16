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
			Item.width = Item.height = 20;
			Item.rare = ItemRarityID.White;
			Item.value = Item.sellPrice(copper: 30);

			Item.crit = 2;
			Item.damage = 3;
			Item.knockBack = .5f;

			Item.useTime = Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
			// item.useTurn = false;
			// item.autoReuse = false;
			
			Item.shootSpeed = 7f;
			Item.useAmmo = ModContent.ItemType<Materials.Nut>();
			Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.NutRocketProjectile>();		
			Item.UseSound = SoundID.Item61;
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
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Materials.Nut>(), 16).AddIngredient(ModContent.ItemType<Materials.Leaf>(), 4).AddTile(TileID.WorkBenches).Register();
		}
	}
}
