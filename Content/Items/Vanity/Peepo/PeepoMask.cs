using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Vanity.Peepo
{
	[AutoloadEquip(EquipType.Head)]
	public class PeepoMask : ModItem
	{
		public override string Texture => Assets.Items.Peepo + "PeepoMask";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Peepo's Mask");
			Tooltip.SetDefault("'Great for impersonating sig!'");
		}

		public override void SetDefaults()
		{
			item.width = item.height = 22;
			item.value = Item.sellPrice(0);
			item.rare = ItemRarityID.Cyan;
			item.vanity = true;
		}
	}
}