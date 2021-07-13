using TrelamiumTwo.Common.Players;
using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Core;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Everbloom
{
	[AutoloadEquip(EquipType.Head)]
	public class BlossomHood : ModItem
	{
		public override string Texture => Assets.Armors.BloomRose + "BlossomHood";

		public override void SetDefaults()
		{
			item.defense = 2;

			item.width = 22;
			item.height = 20;

			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 1, copper: 20);
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
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<BloomRose>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Leaf>(), 8);
			recipe.AddIngredient(ItemID.Wood, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}