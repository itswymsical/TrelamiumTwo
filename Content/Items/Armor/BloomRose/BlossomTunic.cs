using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Core;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.BloomRose
{
	[AutoloadEquip(EquipType.Body)]
	public class BlossomTunic : ModItem
	{
		public override string Texture => Assets.Armor.BloomRose + "BlossomTunic";

		public override void SetDefaults()
		{
			item.defense = 2;

			item.width = 34;
			item.height = 18;

			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 1, copper: 80);
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms) => drawHands = true;

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Materials.BloomRose>(), 4);
			recipe.AddIngredient(ModContent.ItemType<Leaf>(), 8);
			recipe.AddIngredient(ItemID.Wood, 30);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}