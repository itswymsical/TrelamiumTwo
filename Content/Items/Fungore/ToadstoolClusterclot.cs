using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Content.Items.Fungore
{
	public class ToadstoolClusterclot : TrelamiumItem
	{
		public override void SetStaticDefaults() 
			=> DisplayName.SetDefault("Toadstool Clusterclot");
		public override void SetDefaults()
		{
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(silver: 5);

			item.crit = 2;
			item.damage = 13;
			item.knockBack = 5f;

			item.useTime = item.useAnimation = 26;
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.noUseGraphic = 
				item.ranged = 
				item.noMelee = 
				item.channel = true;

			item.autoReuse = false;

			item.shootSpeed = 8f;
			item.shoot = ModContent.ProjectileType<Projectiles.Ranged.ToadstoolClutserclotProjectile>();
			
			item.UseSound = SoundID.Item1;
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
