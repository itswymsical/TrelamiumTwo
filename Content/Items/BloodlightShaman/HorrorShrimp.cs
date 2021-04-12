using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.BloodlightShaman
{
	public class HorrorShrimp : TrelamiumItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Gruesome Goat Horn");
			Tooltip.SetDefault("Summons the Bloodlight Shaman");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 12;
		}

		public override void SetDefaults() 
		{
			item.maxStack = 20;
			item.value = 0;

			item.useAnimation = 30;
			item.useTime = 30;
			item.consumable = true;

			item.rare = ItemRarityID.Blue;
			item.useStyle = ItemUseStyleID.HoldingUp;
		}

		public override bool CanUseItem(Player player) {
			return !NPC.AnyNPCs(ModContent.NPCType<NPCs.BloodlightShaman.BloodlightShaman>());
		}

		public override bool UseItem(Player player) {
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<NPCs.BloodlightShaman.BloodlightShaman>());
			Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}
	}
}