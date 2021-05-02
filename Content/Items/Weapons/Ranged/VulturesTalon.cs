using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Weapons.Ranged
{
    public class VulturesTalon : ModItem
    {

		public override void SetStaticDefaults() => DisplayName.SetDefault("Vultures Talon");

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 62;
			item.rare = ItemRarityID.Green;
			item.value = Item.sellPrice(silver: 49);

			item.damage = 9;
			item.knockBack = 2f;

			item.useTime = 10;
			item.useAnimation = 30;
			item.reuseDelay = 14;

			item.useStyle = ItemUseStyleID.HoldingOut;

			item.ranged = item.noMelee = true;
			
			item.autoReuse = false;

			item.shootSpeed = 8f;
			item.useAmmo = AmmoID.Arrow;
			item.shoot = ProjectileID.WoodenArrowFriendly;

			item.UseSound = SoundID.Item5;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if (type == ProjectileID.WoodenArrowFriendly)
                type = ModContent.ProjectileType<Projectiles.Ranged.VulturesTalon>();
			
			return true;
        }

    }
}
