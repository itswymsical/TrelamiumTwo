#region Using directives

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Items.Materials
{
    public sealed class DesolateHusk : TrelamiumItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 18;
            item.maxStack = 999;
            item.rare = ItemRarityID.White;
            item.value = Item.buyPrice(copper: 50);

			item.material = true;
        }
    }
}
