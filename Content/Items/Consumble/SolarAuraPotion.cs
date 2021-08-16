using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Consumable
{
	public class SolarAuraPotion : ModItem
	{
		public override string Texture => Assets.Items.Consumable + "SolarAuraPotion";
		public override void SetStaticDefaults() 
			=> Tooltip.SetDefault("Increases the damage of fire related buffs and increases defense while in lava");
		
		public override void SetDefaults()
		{
			Item.width = Item.height = 20;
			Item.maxStack = 30;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.buyPrice(silver: 2);

			Item.useTime = Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.EatFood;

			Item.useTurn = true;
			Item.consumable = true;

			Item.buffTime = 10800;
			Item.buffType = ModContent.BuffType<Buffs.Potions.SolarAuraPotion>();

			Item.UseSound = SoundID.Item3;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.Bottle).AddIngredient(ItemID.Daybloom).AddIngredient(ItemID.Fireblossom).AddIngredient(ModContent.ItemType<Fish.Sunfish>()).AddTile(TileID.AlchemyTable).Register();
		}
	}
}
