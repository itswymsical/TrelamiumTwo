using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Content.Projectiles.Ranged;

namespace TrelamiumTwo.Content.Items.Weapons.Ancient
{
    public class VulturesTalon : TrelamiumItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vulture's Talon");
            Tooltip.SetDefault("Every 3rd arrow creates a sandstorm at the location of it's death");
        }
        public override void SafeSetDefaults()
        {
            item.damage = 7;
            item.knockBack = .5f;

            item.ranged =
                item.noMelee = true;
            item.autoReuse = false;

            item.useTime = item.useAnimation = 27;
            item.width = item.height = 52;

            item.useAmmo = AmmoID.Arrow;
            item.shootSpeed = 5.5f;
            item.shoot = ProjectileID.WoodenArrowHostile;

            item.rare = ItemRarityID.White;
            item.UseSound = SoundID.Item5;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.value = Item.sellPrice(copper: 28);
        }
        int num;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            num++;
            if (num >= 3)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<TalonArrowProjectile>(), damage, knockBack, player.whoAmI);
                num = 0;
            }
            return true;
        }
    }
}