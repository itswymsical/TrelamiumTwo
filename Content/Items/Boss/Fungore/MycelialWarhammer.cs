using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Boss.Fungore
{
	public class MycelialWarhammer : ModItem
	{
		public override string Texture => Assets.Items.Fungore + "MycelialWarhammer";
		public override void SetDefaults()
		{
			Item.rare = ItemRarityID.White;
			Item.value = Item.sellPrice(silver: 5);

			Item.crit = 5;
			Item.damage = 18;
			Item.knockBack = 5f;

			Item.useTime = Item.useAnimation = 26;
			Item.useStyle = ItemUseStyleID.Shoot;

			Item.noUseGraphic = 
				Item.DamageType = 
				// item.noMelee = 
				item.channel = true;

			// item.autoReuse = false;

			Item.shootSpeed = 6f;
			Item.shoot = ModContent.ProjectileType<Projectiles.Melee.MycelialWarhammer>();		
			Item.UseSound = SoundID.DD2_MonkStaffSwing;
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

			return false;
		}
	}
}
