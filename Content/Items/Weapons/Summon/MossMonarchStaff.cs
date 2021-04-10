using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Weapons.Summon
{
    public class MossMonarchStaff : TrelamiumItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moss Monarch Staff");
            Tooltip.SetDefault("Summons Moss Hornet to fight for you" +
            "\nHoly! He's an inferno!");
        }
        public override void SetDefaults()
        {
            item.summon = true;
            item.noMelee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.useAnimation = 25;
            item.useTime = 25;
            item.damage = 19;
            item.crit = 3;

            item.knockBack = 1.75f;

            item.rare = ItemRarityID.Cyan;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item44;
            item.shoot = ModContent.ProjectileType<Projectiles.Summon.MossMonarchProjectile>();
            item.value = Item.buyPrice(gold: 10);
        }
    }
}