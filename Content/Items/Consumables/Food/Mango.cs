#region Using directives

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Items;
#endregion

namespace TrelamiumTwo.Content.Items.Consumables.Food
{
	public sealed class Mango : FoodItem
	{
		public override void SetDefaults()
		{
			item.maxStack = 30;
			item.rare = ItemRarityID.Blue;
			item.value = Item.buyPrice(silver: 2);

			item.useTime = item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.EatingUsing;

			item.useTurn = true;
			item.consumable = true;

			item.UseSound = SoundID.Item3;
		}
        public override void OnConsumeItem(Player player)
        {
			CombatText.NewText(player.Hitbox, Color.Orange, "Life Regeneration [II]", true, false);
			player.AddBuff(BuffID.Regeneration, 600);
        }
    }
}
