using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Consumables
{
	public class SolarAuraPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases the damage of fire related buffs and increases defense while in lava");
		}
		public override void SetDefaults()
		{
			item.width = item.height = 20;
			item.maxStack = 30;
			item.rare = ItemRarityID.Blue;
			item.value = Item.buyPrice(silver: 2);

			item.useTime = item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.EatingUsing;

			item.useTurn = true;
			item.consumable = true;

			item.buffTime = 10800;
			item.buffType = ModContent.BuffType<Buffs.Potions.SolarAuraPotion>();

			item.UseSound = SoundID.Item3;
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bottle);
			recipe.AddIngredient(ItemID.Daybloom);
			recipe.AddIngredient(ItemID.Fireblossom);
			recipe.AddIngredient(ModContent.ItemType<Fish.Sunfish>());
			recipe.AddTile(TileID.AlchemyTable);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
