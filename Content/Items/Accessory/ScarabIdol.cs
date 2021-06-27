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
			item.rare = ItemRarityID.Blue;
			item.accessory = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxMinions++;
			player.GetModPlayer<Common.Players.TrelamiumPlayer>().scarabIdol = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Materials.DesolateHusk>(), 5);
			recipe.AddIngredient(ModContent.ItemType<Materials.CrackedScarabHorn>(), 2);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
