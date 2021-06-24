using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Core;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Antlion
{
	[AutoloadEquip(EquipType.Body)]
	public class AntlionBreastplate : ModItem
	{
		public override string Texture => Assets.Items.Antlion + "AntlionBreastplate";
		public override void SetStaticDefaults() => Tooltip.SetDefault("Increases damage by 4%");
        public override void SetDefaults()
		{
			item.defense = 3;

			item.width = 34;
			item.height = 18;

			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(silver: 2, copper: 80);
		}
		public override void UpdateEquip(Player player) => player.allDamage += .4f;
        public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<HardenedCarapace>(), 2);
			recipe.AddIngredient(ModContent.ItemType<AntlionChitin>(), 3);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}