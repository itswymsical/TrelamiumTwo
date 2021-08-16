using TrelamiumTwo.Common.Players;
using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Core;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.BloomRose
{
	[AutoloadEquip(EquipType.Head)]
	public class BlossomHood : ModItem
	{
		public override string Texture => Assets.Armor.BloomRose + "BlossomHood";

		public override void SetDefaults()
		{
			Item.defense = 2;

			Item.width = 22;
			Item.height = 20;

			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 1, copper: 20);
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<BlossomTunic>() && legs.type == ModContent.ItemType<BlossomLeggings>();

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Increases maximum mana by 20 and mana regeneration by 5";

			player.statManaMax2 += 20;
			player.manaRegen += 5;
			player.GetModPlayer<ArmorSetPlayer>().EverbloomSet = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Materials.BloomRose>(), 1).AddIngredient(ModContent.ItemType<Leaf>(), 8).AddIngredient(ItemID.Wood, 20).AddTile(TileID.Anvils).Register();
		}
	}
}