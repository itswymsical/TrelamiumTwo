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
			item.width = item.height = 44;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(copper: 20);

			item.crit = 2;
			item.damage = 8;
			item.knockBack = 6f;

			item.useTime = item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.melee =
				item.noMelee =
				item.channel =
				item.noUseGraphic = true;

			item.shootSpeed = 7f;

			item.shoot = ModContent.ProjectileType<Projectiles.Melee.WoodDaibo>();
			item.UseSound = SoundID.Item1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position, Vector2.Zero, type, damage, knockBack, player.whoAmI, new Vector2(speedX, speedY).ToRotation());
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 15);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
