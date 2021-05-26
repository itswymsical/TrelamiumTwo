using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Items;

namespace TrelamiumTwo.Content.Items.Tools.Shovels
{
    public class Bottomfeeder : ShovelItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Bottomfeeder");
            Tooltip.SetDefault("Has a chance to dig up a random ore");
        }
        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 6;
            item.useTime = 22;
            item.useAnimation = 22;
            item.rare = ItemRarityID.Blue;
            DiggingPower(60);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;

            item.value = Item.sellPrice(silver: 3);
            item.useTurn = true;

            item.UseSound = SoundID.Item18;
        }
    }
}