using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Core;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Sandcrawler
{
	[AutoloadEquip(EquipType.Legs)]
	public class SandcrawlerLeggings : ModItem
	{
		public override string Texture => Assets.Items.Sandcrawler + "SandcrawlerLeggings";
		public override void SetStaticDefaults() => Tooltip.SetDefault("Increases movement speed by 4%");
		public override void SetDefaults()
		{
			item.defense = 2;

			item.width = 26;
			item.height = 18;

			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 2);
		}
		public override void UpdateEquip(Player player) => player.moveSpeed += .04f;
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<HardenedCarapace>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AntlionChitin>(), 4);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}