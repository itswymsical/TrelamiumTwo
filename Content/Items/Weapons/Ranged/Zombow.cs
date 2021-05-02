using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Weapons.Ranged
{
    public class Zombow : TrelamiumItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zombow");
			Tooltip.SetDefault("Converts Wooden Arrows into Zombie Arms");
		}

		public override void SetDefaults()
		{
			item.rare = ItemRarityID.Green;
			item.value = Item.sellPrice(silver: 49);

			item.damage = 9;
			item.knockBack = 2f;

			item.useTime = item.useAnimation = 28;
			
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.ranged = item.noMelee = true;
			
			item.autoReuse = false;

			item.shootSpeed = 8f;
			item.useAmmo = AmmoID.Arrow = item.shoot = ProjectileID.WoodenArrowFriendly;
			
			item.UseSound = SoundID.Item5;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if (type == ProjectileID.WoodenArrowFriendly)
				type = ModContent.ProjectileType<Projectiles.Ranged.ZombieArmProjectile>();
			
			return true;
        }

    }
}
