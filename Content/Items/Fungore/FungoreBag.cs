using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Fungore
{
	public class FungoreBag : TrelamiumItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.Expert;
			item.expert = true;
		}
		public override bool CanRightClick()
		{
			return true;
		}
		public override int BossBagNPC 
			=> ModContent.NPCType<NPCs.Fungore.Fungore>();
		public override void OpenBossBag(Player player)
		{
			//int choice = Main.rand.Next(2);

			player.QuickSpawnItem(ModContent.ItemType<MycelialWarhammer>());

		}
	}
}