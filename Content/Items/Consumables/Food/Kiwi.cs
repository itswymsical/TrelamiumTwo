using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

using TrelamiumTwo.Common.Items;


namespace TrelamiumTwo.Content.Items.Consumables.Food
{
	public sealed class Kiwi : FoodItem
	{
		public override void SetStaticDefaults()
			=> DisplayName.SetDefault("Kiwi");
        public override void SetDefaults()
		{
			item.maxStack = 30;
			item.rare = ItemRarityID.White;
			item.value = Item.buyPrice(silver: 2);

			item.useTime = item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.EatingUsing;
			ExpireTimer = 12000;

			item.potion = true;
			item.useTurn = true;
			item.consumable = true;
			item.healLife = 75;
			item.UseSound = SoundID.Item3;
		}
        public override void OnConsumeItem(Player player)
        {
			var Index = CombatText.NewText(player.Hitbox, Color.LimeGreen, "Healing [II]", true, false);
			Main.combatText[Index].lifeTime = 120;
		}
    }
}
