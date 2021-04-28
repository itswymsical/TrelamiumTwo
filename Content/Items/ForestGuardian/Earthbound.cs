using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.ForestGuardian
{
    public class Earthbound : TrelamiumItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Earthbound");
			Tooltip.SetDefault("Converts Wooden Arrows into Earthen Arrows");
		}

		public override void SetDefaults()
		{
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 80);

			item.damage = 9;
			item.knockBack = 2f;

			item.useTime = 32;
			item.useAnimation = 32;

			item.useStyle = ItemUseStyleID.HoldingOut;

			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = false;

			item.shootSpeed = 8f;
			item.useAmmo = AmmoID.Arrow;
			item.shoot = ProjectileID.WoodenArrowFriendly;

			item.UseSound = SoundID.Item5;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if (type == ProjectileID.WoodenArrowFriendly)
				type = ModContent.ProjectileType<Projectiles.Ranged.ToadstoolProjectile>();
			
			return true;
        }
	}
}
