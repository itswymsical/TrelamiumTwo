using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Consumable.Food
{
	public class Carrot : ModItem
	{
		public override string Texture => Assets.Items.Food + "Carrot";
		public override void SetDefaults()
		{
			Item.width = Item.height = 20;
			Item.maxStack = 30;
			Item.rare = ItemRarityID.White;
			Item.value = Item.buyPrice(copper: 25);

			Item.useTime = Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.EatFood;

			Item.useTurn = true;
			Item.consumable = true;

			Item.buffTime = 900;
			Item.buffType = BuffID.Spelunker;

			Item.UseSound = SoundID.Item3;
		}
        public override void OnConsumeItem(Player player) => CombatText.NewText(player.getRect(), Color.Orange, "Spelunker [30s]", true, false);	
    }
}
