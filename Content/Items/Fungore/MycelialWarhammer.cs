using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Content.Items.Fungore
{
	public class MycelialWarhammer : TrelamiumItem
	{
		public override void SetStaticDefaults() 
			=> DisplayName.SetDefault("Mycelial Warhammer");
		public override void SetDefaults()
		{
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(silver: 5);

			item.crit = 2;
			item.damage = 10;
			item.knockBack = 5f;

			item.useTime = item.useAnimation = 26;
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.melee = true;
			item.noMelee = true;
			item.channel = true;
			item.autoReuse = false;
			item.noUseGraphic = true;

			item.shootSpeed = 6f;
			item.shoot = ModContent.ProjectileType<Projectiles.Melee.MycelialWarhammerProjectile>();
			
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
