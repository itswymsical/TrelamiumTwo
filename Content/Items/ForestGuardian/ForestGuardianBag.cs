using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.ForestGuardian
{
	public class ForestGuardianBag : TrelamiumItem
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
		public override bool CanRightClick() => true;
		
		public override int BossBagNPC 
			=> ModContent.NPCType<NPCs.Fungore.Fungore>();
		public override void OpenBossBag(Player player)
		{
			int choice = Main.rand.Next(3);
			if (Main.rand.Next(7) == 0)
            {
				player.QuickSpawnItem(ModContent.ItemType<ForestGuardianMask>());
			}
			if (choice == 0)
				player.QuickSpawnItem(ModContent.ItemType<Earthbound>());
			if (choice == 1)
				player.QuickSpawnItem(ModContent.ItemType<AlluvialCollider>());
			if (choice == 2)
				player.QuickSpawnItem(ModContent.ItemType<TheAncient>());
		}
	}
}