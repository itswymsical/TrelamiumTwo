using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Trelamium2.Content.Items.Armor.Vanity.Peepo
{
	[AutoloadEquip(EquipType.Legs)]
	public class SigniasLegs : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Signia's Froggy Feet");
			Tooltip.SetDefault("'Great for impersonating developers!'");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = 0;
			item.rare = ItemRarityID.Cyan;
			item.vanity = true;
		}
	}
}