using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

using TrelamiumTwo.Common.Items;

namespace TrelamiumTwo.Content.Items.Consumables.Food
{
	public sealed class CarrotBundle : FoodItem
	{
		public override void SetStaticDefaults()
		=> DisplayName.SetDefault("Bundle of Carrots");
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

			item.buffType = BuffID.ManaRegeneration;
			item.buffTime = 8000;
			item.UseSound = SoundID.Item3;
		}
        public override void OnConsumeItem(Player player)
        {
			var Index = CombatText.NewText(player.Hitbox, Color.Orange, "Night Vision [VI]", true, false);
			Main.combatText[Index].lifeTime = 120;
		}
    }
}
