using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Vanity.Peepo
{
	[AutoloadEquip(EquipType.Body)]
	public class SigniasShirt : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Signia's Shirt");
			Tooltip.SetDefault("'Great for impersonating developers!'");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 18;
			item.value = 0;
			item.rare = ItemRarityID.Cyan;
			item.vanity = true;
		}
	}
}