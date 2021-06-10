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

			item.damage = 10;
			item.knockBack = 2f;
			item.shootSpeed = 8f;

			item.useTime = item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.ranged = item.noMelee = true;
			item.autoReuse = false;

			item.useAmmo = AmmoID.Arrow;
			item.shoot = ProjectileID.WoodenArrowFriendly;
			item.UseSound = SoundID.Item5;
		}
		public override Vector2? HoldoutOffset()
			=> new Vector2(-5, 0);
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if (type == ProjectileID.WoodenArrowFriendly)
				type = ModContent.ProjectileType<Projectiles.Ranged.EarthboundArrowProjectile>();
			
			return true;
        }
	}
}
