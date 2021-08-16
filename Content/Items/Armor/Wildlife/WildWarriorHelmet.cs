using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;
using TrelamiumTwo.Content.Items.Materials;

namespace TrelamiumTwo.Content.Items.Armor.WildWarrior
{
	[AutoloadEquip(EquipType.Head)]
	public class WildWarriorHelmet : ModItem
	{
		public override string Texture => Assets.Armor.Wildlife + "WildWarriorHelmet";
		public override void SetStaticDefaults() => DisplayName.SetDefault("Wild Warrior Helmet");

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 22;

			Item.defense = 2;

			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 3);
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<WildWarriorGarb>() && legs.type == ModContent.ItemType<WildWarriorGreaves>();

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+2 defense";
			player.statDefense += 2;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<WildlifeFragment>(), 6).AddTile(TileID.LivingLoom).Register();
		}
	}
}
