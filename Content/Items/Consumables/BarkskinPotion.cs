#region Using directives

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Items.Consumables
{
	public sealed class BarkskinPotion : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases defense by 15%\nLowers jump height by 25%");
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

			item.buffTime = 14400;
			item.buffType = ModContent.BuffType<Buffs.Potions.BarkskinPotion>();

			item.UseSound = SoundID.Item3;
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(ModContent.ItemType<Fish.Barkfish>());
			recipe.AddIngredient(ItemID.Daybloom, 2);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
