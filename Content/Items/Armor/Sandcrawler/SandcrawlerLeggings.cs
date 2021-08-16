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
		public override string Texture => Assets.Armor.Sandcrawler + "SandcrawlerLeggings";
		public override void SetStaticDefaults() => Tooltip.SetDefault("Increases movement speed by 4%");
		public override void SetDefaults()
		{
			Item.defense = 2;

			Item.width = 26;
			Item.height = 18;

			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 2);
		}
		public override void UpdateEquip(Player player) => player.moveSpeed += .04f;
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<HardenedCarapace>(), 1).AddIngredient(ModContent.ItemType<AntlionChitin>(), 4).AddTile(TileID.Anvils).Register();
		}
	}
}