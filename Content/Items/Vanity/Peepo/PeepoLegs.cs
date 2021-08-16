using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Vanity.Peepo
{
	[AutoloadEquip(EquipType.Legs)]
	public class PeepoLegs : ModItem
	{
		public override string Texture => Assets.Vanity.Peepo + "PeepoLegs";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Peepo's Legs");
			Tooltip.SetDefault("'Great for impersonating sig!'");
		}

		public override void SetDefaults()
		{
			Item.width = Item.height = 22;
			Item.value = Item.sellPrice(0);
			Item.rare = ItemRarityID.Cyan;
			Item.vanity = true;
		}
	}
}