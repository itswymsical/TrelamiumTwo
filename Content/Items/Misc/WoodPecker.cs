using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Misc
{
	public class WoodPecker : ModItem
	{
		public override string Texture => Assets.Items.Misc + "WoodPecker";
		public override void SetDefaults()
		{
			Item.width = Item.height = 12;
			Item.maxStack = 999;

			Item.useTime = 10;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;

			Item.useTurn = true;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.noUseGraphic = true;

			Item.makeNPC = (short)ModContent.NPCType<NPCs.Critters.WoodPecker>();
		}
	}
}
