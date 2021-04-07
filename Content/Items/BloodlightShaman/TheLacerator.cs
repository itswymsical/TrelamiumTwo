using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

namespace Trelamium2.Content.Items.BloodlightShaman
{
	public sealed class TheLacerator : TrelamiumItem
	{
		public override void SetDefaults() // Eldrazi Code imported from EH
		{
			item.width = 62;
			item.height = 72;
			item.rare = ItemRarityID.LightRed;
			item.value = Item.sellPrice(gold: 5, silver: 25);

			item.crit = 4;
			item.damage = 35;
			item.knockBack = 4.75f;

			item.useTime = item.useAnimation = 26;
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.melee = true;
			item.noMelee = true;
			item.channel = true;
			item.autoReuse = false;
			item.noUseGraphic = true;

			item.shootSpeed = 6f;
			item.shoot = ModContent.ProjectileType<Projectiles.Melee.TheLaceratorProjectile>();
			
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
