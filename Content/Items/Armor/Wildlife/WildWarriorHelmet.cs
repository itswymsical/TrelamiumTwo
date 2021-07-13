using TrelamiumTwo.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.WildWarrior
{
	[AutoloadEquip(EquipType.Head)]
	public class WildWarriorHelmet : ModItem
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Wild Warrior Helmet");

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 22;

			item.defense = 2;

			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 3);
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<WildWarriorGarb>() && legs.type == ModContent.ItemType<WildWarriorGreaves>();

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+2 defense";
			player.statDefense += 2;
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<WildlifeFragment>(), 6);
			recipe.AddTile(TileID.LivingLoom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
