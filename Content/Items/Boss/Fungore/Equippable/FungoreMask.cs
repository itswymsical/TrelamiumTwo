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
            Item.value = Item.sellPrice(0);
            Item.rare = ItemRarityID.Blue;
            Item.vanity = true;
		}
    }
}