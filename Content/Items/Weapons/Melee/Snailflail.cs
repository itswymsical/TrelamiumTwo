using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Content.Items.Weapons.Melee
{
	public class Snailflail : TrelamiumItem
	{		
		public override void SetDefaults()
		{
			item.width = item.height = 40;
			item.rare = ItemRarityID.Orange;
			item.value = Item.buyPrice(0, 0, 4, 66);
			
			item.crit = 8;
			item.damage = 30;
			item.knockBack = 6f;
			
			item.useTime = 20;
			item.useAnimation = 40;
			item.useStyle = ItemUseStyleID.HoldingOut;
			
			item.melee = true;
			item.noMelee = true;
			item.channel = true;			
			item.noUseGraphic = true;
			
			item.shootSpeed = 12f;
			item.shoot = ModContent.ProjectileType<Projectiles.Melee.SnailflailProjectile>();
			
			item.UseSound = SoundID.Item1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position, Vector2.Zero, type, damage, knockBack, player.whoAmI, new Vector2(speedX, speedY).ToRotation());
			return (false);
		}
	}
}
