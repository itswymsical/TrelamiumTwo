using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.AwakeCheeks
{
	public sealed class TomeOfCheekiness : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tome of Cheekiness");
			Tooltip.SetDefault("Charge to shoot magic infused nuts and acorns");
		}
		public override void SetDefaults()
		{
			item.width = item.height = 16;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(0, 0, 60, 0);

			item.mana = 10;
			item.damage = 10;
			item.knockBack = 5;
			
			item.useTime = 7;
			item.useAnimation = 15;
			item.reuseDelay = item.useAnimation + 6;
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.magic = true;
			item.noMelee = true;
			item.autoReuse = true;

			item.shootSpeed = 4f;
			item.shoot = ModContent.ProjectileType<Projectiles.Magic.MagicAcorn>();
			
			item.UseSound = SoundID.Item9;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float minDist = 20f;
			float maxDist = 60f;
			float randomRot = Main.rand.NextFloat() * MathHelper.TwoPi;

			Vector2 spawnPos = position + randomRot.ToRotationVector2() * MathHelper.Lerp(minDist, maxDist, Main.rand.NextFloat());
			
			for (int i = 0; i < 50; ++i)
			{
				spawnPos = position + randomRot.ToRotationVector2() * MathHelper.Lerp(minDist, maxDist, Main.rand.NextFloat());
				if (Collision.CanHit(position, 0, 0, spawnPos + (spawnPos - position).SafeNormalize(Vector2.UnitX) * 8f, 0, 0))
				{
					break;
				}
				randomRot = Main.rand.NextFloat() * MathHelper.TwoPi;
			}
			Projectile.NewProjectile(spawnPos, Vector2.Zero, type, damage, knockBack, player.whoAmI, Main.MouseWorld.X, Main.MouseWorld.Y);

			return (false);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 12);
			recipe.AddIngredient(ItemID.Acorn, 15);
			recipe.AddIngredient(ModContent.ItemType<Materials.Nut>(), 15);
			recipe.AddIngredient(ModContent.ItemType<Materials.Gluttonfur>(), 15);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
