using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Everbloom
{
	[AutoloadEquip(EquipType.Body)]
	public class EverbloomTunic : ModItem
	{
		public override string Texture => Assets.Items.Everbloom + "EverbloomTunic";

		public override void SetDefaults()
		{
			item.defense = 2;

			item.width = 34;
			item.height = 18;

			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(silver: 1, copper: 80);
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms) => drawHands = true;

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 10);
			recipe.AddIngredient(ModContent.ItemType<BloomRose>(), 3);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}