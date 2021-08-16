using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Accessory
{
	public class ScarabIdol : ModItem
	{
		public override string Texture => Assets.Items.Accessory + "ScarabIdol";
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases your max number of minions by 1." +
				"\nMinions inflict poisoned for a short time on hit.");
		}
		public override void SetDefaults()
		{
			Item.rare = ItemRarityID.Blue;
			Item.accessory = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxMinions++;
			player.GetModPlayer<Common.Players.TrelamiumPlayer>().scarabIdol = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Materials.DesolateHusk>(), 5).AddIngredient(ModContent.ItemType<Materials.CrackedScarabHorn>(), 2).AddTile(TileID.TinkerersWorkbench).Register();
		}
	}
}
