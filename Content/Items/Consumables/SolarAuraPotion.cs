#region Using directives

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Items.Consumables
{
	public sealed class SolarAuraPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Infused with the power of the sun...");
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
			recipe.AddIngredient(ItemID.Daybloom);
			recipe.AddIngredient(ItemID.Fireblossom);
			recipe.AddIngredient(ModContent.ItemType<Fish.Sunfish>());
			recipe.AddTile(TileID.AlchemyTable);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
