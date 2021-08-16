using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Weapons.Melee
{
    public class WoodDaibo : ModItem
    {
        public override string Texture => Assets.Weapons.Melee + "WoodDaibo";
        public override void SetStaticDefaults() => Tooltip.SetDefault("Destroys weak projectiles that come into contact");
		public override void SetDefaults()
		{
			Item.width = Item.height = 44;
			Item.rare = ItemRarityID.White;
			Item.value = Item.sellPrice(copper: 20);

			Item.crit = 2;
			Item.damage = 8;
			Item.knockBack = 6f;

			Item.useTime = Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;

			Item.DamageType =
				// item.noMelee =
				item.channel =
				item.noUseGraphic = true;

			Item.shootSpeed = 7f;

			Item.shoot = ModContent.ProjectileType<Projectiles.Melee.WoodDaibo>();
			Item.UseSound = SoundID.Item1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position, Vector2.Zero, type, damage, knockBack, player.whoAmI, new Vector2(speedX, speedY).ToRotation());
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.Wood, 15).AddTile(TileID.WorkBenches).Register();
		}
	}
}
