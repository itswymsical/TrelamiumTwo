#region Using directives

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Items.Accessories
{
	// TODO: Eldrazi - Add correct sprites and autoload.
	//[AutoloadEquip(EquipType.Shoes)]
	public sealed class DustrollerSkates : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Allows you to safely walk over Soot Shale");
		}
		public override void SetDefaults()
		{
			item.width = item.height = 22;
			item.rare = ItemRarityID.Blue;
			
			item.accessory = true;
		}

		public override void UpdateEquip(Player player)
			=> player.GetModPlayer<Common.Players.TrelamiumPlayer>().dustrollerSkates = true;

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.OldShoe);
			recipe.AddIngredient(ItemID.GoldBar, 10);
			recipe.AddIngredient(ModContent.ItemType<Materials.DustiliteCrystal>(), 15);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
