using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Content.Items.AwakeCheeks
{
	public class KarukaChainBlaster : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 42;
			item.height = 22;
			item.rare = ItemRarityID.White;
			item.value = Item.buyPrice(silver: 13);

			item.crit = 4;
			item.damage = 3;
			item.knockBack = 5;
			
			item.useTime = item.useAnimation = 8;
			item.useStyle = ItemUseStyleID.HoldingOut;
			
			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = true;
			
			item.shootSpeed = 6f;
			item.useAmmo = AmmoID.Bullet;
			item.shoot = ProjectileID.Bullet;
			
			item.UseSound = SoundID.Item11;
		}

		public override bool ConsumeAmmo(Player player)
			=> Main.rand.NextFloat() >= 0.25f;

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, -12, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

			if (Main.rand.NextBool(5))
			{
				int otherType = ModContent.ProjectileType<Projectiles.Ranged.NutBullet>();

				if (Main.rand.NextBool(2))
				{
					otherType = ModContent.ProjectileType<Projectiles.Ranged.AcornBullet>();
				}

				Vector2 velocity = new Vector2(speedX, speedY);
				Projectile.NewProjectile(position, velocity.RotatedByRandom(MathHelper.PiOver2 / 3), otherType, damage, knockBack, player.whoAmI);
			}

			return true;
		}

		public override Vector2? HoldoutOffset()
			=> new Vector2(0, -6);

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 8);
			recipe.AddIngredient(ItemID.Acorn, 10);
			recipe.AddIngredient(ItemID.Minishark);
			recipe.AddIngredient(ModContent.ItemType<Materials.Nut>(), 12);
			recipe.AddIngredient(ModContent.ItemType<Materials.Gluttonfur>(), 15);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
