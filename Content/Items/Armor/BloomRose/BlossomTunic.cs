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
			Item.defense = 2;

			Item.width = 34;
			Item.height = 18;

			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 1, copper: 80);
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms) => drawHands = true;

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Materials.BloomRose>(), 4).AddIngredient(ModContent.ItemType<Leaf>(), 8).AddIngredient(ItemID.Wood, 30).AddTile(TileID.Anvils).Register();
		}
	}
}