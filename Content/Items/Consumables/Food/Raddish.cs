using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

using TrelamiumTwo.Common.Items;

namespace TrelamiumTwo.Content.Items.Consumables.Food
{
	public sealed class Raddish : FoodItem
	{
		public override void SetStaticDefaults()
		=> DisplayName.SetDefault("Raddish");
		public override void SetDefaults()
		{
			item.maxStack = 30;
			item.rare = ItemRarityID.White;
			item.value = Item.buyPrice(silver: 2);

			item.useTime = item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.EatingUsing;

			item.useTurn = item.consumable = true;
			ExpireTimer = 12000;

			item.buffTime = 400;
			item.buffType = BuffID.Wrath;
			item.UseSound = SoundID.Item3;
		}
        public override void OnConsumeItem(Player player)
        {
			var Index = CombatText.NewText(player.Hitbox, Color.IndianRed, "Wrath [I]", true, false);
			Main.combatText[Index].lifeTime = 120;
		}
    }
}
