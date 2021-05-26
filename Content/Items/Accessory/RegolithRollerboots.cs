using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Players;

namespace TrelamiumTwo.Content.Items.Accessory
{
	public class RegolithRollerboots : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Allows the wearer to run super fast" +
				"\nAllows you to safely walk over Soot Shale");
		}
		public override void SetDefaults()
		{
			item.width = item.height = 22;
			item.rare = ItemRarityID.Blue;
			
			item.accessory = true;
		}

		public override void UpdateEquip(Player player)
		{
			TrelamiumPlayer trelamiumPlayer = player.GetModPlayer<TrelamiumPlayer>();

			player.accRunSpeed = 6f;
			trelamiumPlayer.dustrollerSkates = true;

			if (trelamiumPlayer.onSand)
			{
				float modifier = 1.75f;
				
				player.maxRunSpeed *= modifier;
				player.accRunSpeed *= modifier;
				player.runSlowdown *= modifier;
				player.runAcceleration *= modifier;
			}
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<DustrollerSkates>());
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
