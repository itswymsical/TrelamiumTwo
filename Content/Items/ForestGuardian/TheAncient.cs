using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Content.Projectiles.Melee;

namespace TrelamiumTwo.Content.Items.ForestGuardian
{
    public class TheAncient : TrelamiumItem
    {

        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("The Ancient");
        

        public override void SetDefaults()
        {
            item.damage = 17;
            item.width = item.height = 54;
            item.useTime = item.useAnimation = 26;
            item.shootSpeed = 3.5f;
            item.melee = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 3f;
            item.rare = ItemRarityID.White;
            item.value = Item.sellPrice(copper: 50);
            item.noUseGraphic = true;
            item.shoot = ModContent.ProjectileType<TheAncientProjectile>();
        }

        public override bool CanUseItem(Player player)
           => player.ownedProjectileCounts[item.shoot] < 1;
    }
}
