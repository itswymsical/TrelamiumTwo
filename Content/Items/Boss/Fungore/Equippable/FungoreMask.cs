using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Boss.Fungore.Equippable
{
	[AutoloadEquip(EquipType.Head)]
	public class FungoreMask : ModItem
    {
        public override string Texture => Assets.Items.Fungore + "Equippable/FungoreMask";
        public override void SetDefaults()
		{
            item.value = Item.sellPrice(0);
            item.rare = ItemRarityID.Blue;
            item.vanity = true;
		}
    }
}