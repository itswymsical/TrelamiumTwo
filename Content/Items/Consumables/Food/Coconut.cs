using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

using TrelamiumTwo.Common.Items;

namespace TrelamiumTwo.Content.Items.Consumables.Food
{
	public sealed class Coconut : FoodItem
	{
		public override void SetStaticDefaults()
			=> DisplayName.SetDefault("Coconut");
        public override void SetDefaults()
		{
			item.maxStack = 30;
			item.rare = ItemRarityID.White;
			item.value = Item.buyPrice(silver: 2);

			item.useTime = item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.EatingUsing;

			item.useTurn = true;
			item.consumable = true;
			ExpireTimer = 12000;

			item.buffType = BuffID.Mining;
			item.buffTime = 600;
			item.UseSound = SoundID.Item3;
		}
        public override void OnConsumeItem(Player player)
        {
			var Index = CombatText.NewText(player.Hitbox, Color.SaddleBrown, "Mining Speed [II]", true, false);
			Main.combatText[Index].lifeTime = 120;
        }
    }
}
