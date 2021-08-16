using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Common.Players;
using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Armor.BloomRose
{
	[AutoloadEquip(EquipType.Head)]
	public class BlossomCrown : ModItem
	{
		public override string Texture => Assets.Armor.BloomRose + "BlossomCrown";
		public override void SetDefaults()
		{
			Item.defense = 2;

			Item.width = 30;
			Item.height = 22;

			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 1, copper: 20);
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair) => drawHair = true;

		public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<BlossomTunic>() && legs.type == ModContent.ItemType<BlossomLeggings>();

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Increases max minions by 1";
			player.maxMinions++;
			player.GetModPlayer<ArmorSetPlayer>().EverbloomSet = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Materials.BloomRose>(), 2).AddIngredient(ModContent.ItemType<Leaf>(), 6).AddIngredient(ItemID.Wood, 20).AddTile(TileID.Anvils).Register();
		}
	}
}