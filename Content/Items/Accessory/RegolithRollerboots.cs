#region Using directives

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Items.Accessory
{
	// TODO: Eldrazi - Add correct sprites and autoload.
	//[AutoloadEquip(EquipType.Shoes)]
	public sealed class RegolithRollerboots : TrelamiumItem
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
			var trelamiumPlayer = player.GetModPlayer<Common.Players.TrelamiumPlayer>();

			player.accRunSpeed = 6f;
			trelamiumPlayer.magicGuantlet = true;
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
			// TODO 1.4: Eldrazi - Set correct recipe.
			var recipe = new ModRecipe(mod);

			recipe.AddIngredient(ModContent.ItemType<DustrollerSkates>());
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
