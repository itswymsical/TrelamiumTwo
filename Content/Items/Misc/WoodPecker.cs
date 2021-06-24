using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Misc
{
	public class WoodPecker : ModItem
	{
		public override void SetDefaults()
		{
			item.width = item.height = 12;
			item.maxStack = 999;

			item.useTime = 10;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;

			item.useTurn = true;
			item.autoReuse = true;
			item.consumable = true;
			item.noUseGraphic = true;

			item.makeNPC = (short)ModContent.NPCType<NPCs.Critters.WoodPecker>();
		}
	}
}
