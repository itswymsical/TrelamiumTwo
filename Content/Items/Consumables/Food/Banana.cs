using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

using TrelamiumTwo.Common.Items;

namespace TrelamiumTwo.Content.Items.Consumables.Food
{
	public sealed class Banana : FoodItem
	{
		public override void SetStaticDefaults()
		=> DisplayName.SetDefault("Banana");
		public override void SetDefaults()
		{
			item.maxStack = 30;
			item.rare = ItemRarityID.White;
			item.value = Item.buyPrice(silver: 2);

			item.useTime = item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.EatingUsing;

			item.useTurn = true;
			item.consumable = true;
			ExpireTimer = 36000;

			item.buffType = BuffID.Spelunker;
			item.buffTime = 6000;
			item.UseSound = SoundID.Item3;
		}
        public override void OnConsumeItem(Player player)
        {
			var Index = CombatText.NewText(player.Hitbox, Color.Yellow, "Spelunker [IV]", true, false);
			Main.combatText[Index].lifeTime = 120;
		}
    }
}
