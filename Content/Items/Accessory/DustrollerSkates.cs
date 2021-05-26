using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Players;

namespace TrelamiumTwo.Content.Items.Accessory
{
	public class DustrollerSkates : TrelamiumItem
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
			=> player.GetModPlayer<TrelamiumPlayer>().dustrollerSkates = true;

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
