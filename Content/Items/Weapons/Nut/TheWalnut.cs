using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Weapons.Nut
{
	public class TheWalnut : ModItem
	{
		public override string Texture => Assets.Weapons.Nut + "TheWalnut";
		public override void SetStaticDefaults() => Tooltip.SetDefault("When used the player swings a full 360 with the Walnut");
        public override void SetDefaults()
		{
			Item.width = Item.height = 44;
			Item.rare = ItemRarityID.White;
			Item.value = Item.sellPrice(copper: 66);
			
			Item.crit = 2;
			Item.damage = 10;
			Item.knockBack = 5f;
			
			Item.useTime = 20;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			
			Item.DamageType = 
				// item.noMelee = 
				item.channel = 
				item.noUseGraphic = true;
			
			Item.shootSpeed = 12f;

			Item.shoot = ModContent.ProjectileType<Projectiles.Melee.TheWalnut>();
			Item.UseSound = SoundID.Item1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position, Vector2.Zero, type, damage, knockBack, player.whoAmI, new Vector2(speedX, speedY).ToRotation());
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Materials.Nut>(), 20).AddIngredient(ModContent.ItemType<Materials.Leaf>(), 6).AddTile(TileID.WorkBenches).Register();
		}
	}
}
