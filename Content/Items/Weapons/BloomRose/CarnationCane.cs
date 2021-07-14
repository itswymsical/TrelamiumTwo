using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Content.Projectiles.Magic;

namespace TrelamiumTwo.Content.Items.Weapons.Magic
{
	public class CarnationCane : ModItem
	{
		public override string Texture => Assets.Weapons.BloomRose + "CarnationCane";
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Releases a burst of petal which create bushes on tile collison");
			Item.staff[item.type] = true;
		}
		
		public override void SetDefaults()
		{
			item.width = item.height = 11;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 80, copper: 30);

			item.mana = 8;
			item.crit = 4;
			item.damage = 19;
			item.knockBack = 3f;

			item.useTime = 10;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.magic = true;
			item.noMelee = true;
			item.autoReuse = false;
			
			item.shootSpeed = 10f;
			item.shoot = ModContent.ProjectileType<CarnationCanePetal>();
			
			item.UseSound = SoundID.Item7;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int currentShot = player.itemAnimation / item.useTime - 1;

			Vector2 velocity = new Vector2(speedX, speedY).RotatedBy(MathHelper.PiOver4 / 2 * currentShot);
			Projectile.NewProjectile(position, velocity, type, damage, knockBack, player.whoAmI);

			return false;
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Materials.BloomRose>(), 8);
			recipe.AddIngredient(ModContent.ItemType<Twig>(), 8);
			recipe.AddTile(TileID.LivingLoom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
