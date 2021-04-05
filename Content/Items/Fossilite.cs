using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Trelamium2.Content.Items
{
    public class Fossilite : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fossilite");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.rare = ItemRarityID.White;
            item.value = Item.buyPrice(0, 0, 10, 0);
        }
    }
}