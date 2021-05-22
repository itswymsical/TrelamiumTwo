using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.ForestGuardian
{
	[AutoloadEquip(EquipType.Head)]
	public class ForestGuardianMask : TrelamiumItem
    {
        public override bool DrawHead() => false;
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Forest Guardian Mask");
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