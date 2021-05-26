using TrelamiumTwo.Common.Players;
using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Everbloom
{
	[AutoloadEquip(EquipType.Head)]
	public class EverbloomHood : ModItem
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Everbloom Hood");

		public override void SetDefaults()
		{
			item.defense = 2;

			item.width = 22;
			item.height = 20;

			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(silver: 1, copper: 20);
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<EverbloomTunic>() && legs.type == ModContent.ItemType<EverbloomLeggings>();

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Increases maximum mana by 20";

			player.statManaMax2 += 20;

			player.GetModPlayer<ArmorSetPlayer>().EverbloomSet = true;
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 5);
			recipe.AddIngredient(ModContent.ItemType<BloomRose>(), 2);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}