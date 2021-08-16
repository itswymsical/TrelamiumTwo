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
			Item.staff[Item.type] = true;
		}
		
		public override void SetDefaults()
		{
			Item.width = Item.height = 11;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 80, copper: 30);

			Item.mana = 8;
			Item.crit = 4;
			Item.damage = 19;
			Item.knockBack = 3f;

			Item.useTime = 10;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Shoot;

			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			// item.autoReuse = false;
			
			Item.shootSpeed = 10f;
			Item.shoot = ModContent.ProjectileType<CarnationCanePetal>();
			
			Item.UseSound = SoundID.Item7;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int currentShot = player.itemAnimation / Item.useTime - 1;

			Vector2 velocity = new Vector2(speedX, speedY).RotatedBy(MathHelper.PiOver4 / 2 * currentShot);
			Projectile.NewProjectile(position, velocity, type, damage, knockBack, player.whoAmI);

			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Materials.BloomRose>(), 8).AddIngredient(ModContent.ItemType<Twig>(), 8).AddTile(TileID.LivingLoom).Register();
		}
	}
}
