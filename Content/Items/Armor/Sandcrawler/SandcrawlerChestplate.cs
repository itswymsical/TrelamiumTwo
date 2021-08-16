using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Core;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Sandcrawler
{
	[AutoloadEquip(EquipType.Body)]
	public class SandcrawlerChestplate : ModItem
	{
		public override string Texture => Assets.Armor.Sandcrawler + "SandcrawlerChestplate";
		public override void SetStaticDefaults() => Tooltip.SetDefault("Increases movement speed by 4%");
        public override void SetDefaults()
		{
			Item.defense = 3;

			Item.width = 34;
			Item.height = 18;

			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 2, copper: 80);
		}
		public override void UpdateEquip(Player player) => player.moveSpeed += .04f;
        public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<HardenedCarapace>(), 2).AddIngredient(ModContent.ItemType<AntlionChitin>(), 3).AddIngredient(ModContent.ItemType<SandcrawlerShell>(), 2).AddTile(TileID.Anvils).Register();
		}
	}
}