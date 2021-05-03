using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Content.Items.ForestGuardian
{
	public class PrimordialEarth : TrelamiumItem
	{
		public override string Texture => TrelamiumTwo.HeaviesAssets + "PrimordialEarth";
		public override void SetStaticDefaults() 
			=> DisplayName.SetDefault("Primordial Earth");
		public override void SetDefaults()
		{
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 25);

			item.crit = 2;
			item.damage = 22;
			item.knockBack = 8f;

			item.useTime = item.useAnimation = 26;
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.noUseGraphic = item.melee = item.noMelee = item.channel = true;
			item.autoReuse = false;

			item.shootSpeed = 6f;
			item.shoot = ModContent.ProjectileType<Projectiles.Defensive.PrimordialEarthProjectile>();

			item.UseSound = SoundID.DD2_MonkStaffSwing;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int direction = System.Math.Sign(speedX);
			if (direction == 0)
			{
				direction = 1;
			}

			Projectile projectile = Projectile.NewProjectileDirect(position, Vector2.Zero, type, damage, knockBack, player.whoAmI);
			projectile.netUpdate = true;
			projectile.direction = direction;

			return (false);
		}
	}
}
