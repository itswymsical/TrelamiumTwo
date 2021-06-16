using Terraria;
using Terraria.ID;

namespace TrelamiumTwo.Content.Items.Accessory
{
	public class ScarabIdol : ArchaeologicalItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases your max number of minions by 1.\nMinions inflict poisoned for a short time on hit.");
		}
		protected override void SafeSetDefaults()
		{
			item.rare = ItemRarityID.Blue;
			
			item.accessory = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxMinions += 1;
			player.GetModPlayer<Common.Players.TrelamiumPlayer>().scarabIdol = true;
		}
	}
}
