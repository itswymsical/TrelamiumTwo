using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Weapons.Magic
{
    public class MarbleStaff : TrelamiumItem
    {

        public override void SetStaticDefaults() => DisplayName.SetDefault("Marble Staff");

        public override void SetDefaults()
        {
            Item.staff[item.type] = true;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.damage = 23;
            item.knockBack = 2f;
            item.useTime = item.useAnimation = 23;

            item.rare = ItemRarityID.White;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item9;

            item.shoot = ModContent.ProjectileType<Projectiles.Magic.MarbleProjectile>();
            item.shootSpeed = 16f;

            item.value = Item.buyPrice(silver: 80);

            item.width = item.height = 50;
            

        }
    }
}
