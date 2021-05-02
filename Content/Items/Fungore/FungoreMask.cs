using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Fungore
{
	[AutoloadEquip(EquipType.Head)]
	public class FungoreMask : TrelamiumItem
    {
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Fungore Mask");
            Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
            item.value = Item.sellPrice(silver: 1);
            item.rare = ItemRarityID.Blue;
            item.vanity = true;
		}
    }
}