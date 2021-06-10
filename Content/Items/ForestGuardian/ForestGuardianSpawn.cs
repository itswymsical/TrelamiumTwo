using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TrelamiumTwo.Content.Items.ForestGuardian
{
	public class ForestGuardianSpawn : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Forest Altar");
			Tooltip.SetDefault("Summons Forest Guardian");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 12;
		}

		public override void SetDefaults() {
			item.width = 20;
			item.height = 20;
			item.maxStack = 20;
			item.value = 100;
			item.rare = ItemRarityID.Yellow;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.consumable = true;
			item.scale *= .5f;
		}

		public override bool CanUseItem(Player player) 
			=> !NPC.AnyNPCs(NPCType<NPCs.Bosses.ForestGuardian.ForestGuardian>());


		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, NPCType<NPCs.Bosses.ForestGuardian.ForestGuardian>());
			Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}
	}
}