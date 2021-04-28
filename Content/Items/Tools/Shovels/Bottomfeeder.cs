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
            Tooltip.SetDefault("Creates gem shape, gem shape that breaks blocks gives that gem." +
                "\nCode this");
        }
        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 6;
            item.useTime = 22;
            item.useAnimation = 22;
            item.rare = ItemRarityID.Blue;
            diggingPower(60);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;

            item.value = Item.sellPrice(silver: 3);
            item.useTurn = true;

            item.UseSound = SoundID.Item18;
        }
    }
}