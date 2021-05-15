using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Content.Projectiles.Melee;

namespace TrelamiumTwo.Content.Items.Weapons.Melee
{
    public class HorizonWalker : ModItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Horizon Walker");

        public override void SetDefaults()
        {
            ItemID.Sets.Yoyo[item.type] = true;
            ItemID.Sets.GamepadExtraRange[item.type] = 15;
            ItemID.Sets.GamepadSmartQuickReach[item.type] = true;

			item.useStyle = ItemUseStyleID.HoldingOut;
			item.width = 30;
			item.height = 26;
			item.useAnimation = item.useTime = 25;
			
			item.shootSpeed = 16f;
			item.knockBack = 2.5f;
			item.damage = 9;
			item.rare = ItemRarityID.White;

			item.melee = item.channel = item.noMelee = item.noUseGraphic = true;
			
			item.UseSound = SoundID.Item1;
			item.value = Item.sellPrice(silver: 1);
			item.shoot = ModContent.ProjectileType<HorizonWalkerProjectile>();
		}
    }
}
