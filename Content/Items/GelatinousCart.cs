using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items
{
    public class GelatinousCart : TrelamiumItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gelatinous Cart");
            Tooltip.SetDefault("Summons a rideable Slime Minecart (may potentially kill your game)");
        }
        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = item.useTime = 20;
            
            item.mountType = ModContent.MountType<Mounts.KingSlimeCart>();
            item.UseSound = SoundID.Item81;
        }
    }
}