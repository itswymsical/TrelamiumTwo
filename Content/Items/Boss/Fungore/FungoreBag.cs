using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Boss.Fungore
{
	public class FungoreBag : ModItem
	{
		public override string Texture => Assets.Items.Fungore + "FungoreBag";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = Item.height = 24;
			Item.rare = ItemRarityID.Expert;
			Item.expert = true;
		}
		public override bool CanRightClick() => true;
		
		public override int BossBagNPC => ModContent.NPCType<NPCs.Boss.Fungore.Fungore>();
		public override void OpenBossBag(Player player)
		{
			player.QuickSpawnItem(ModContent.ItemType<Equippable.Fungroids>());
			int choice = Main.rand.Next(3);

			if (Main.rand.Next(7) == 0)
				player.QuickSpawnItem(ModContent.ItemType<Equippable.FungoreMask>());
			
			if (choice == 0)
				player.QuickSpawnItem(ModContent.ItemType<MycelialWarhammer>());
			if (choice == 1)
				player.QuickSpawnItem(ModContent.ItemType<ToadstoolClusterclot>());
			if (choice == 2)
				player.QuickSpawnItem(ModContent.ItemType<FunguyStaff>());

		}
	}
}