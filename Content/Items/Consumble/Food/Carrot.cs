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
			item.width = item.height = 20;
			item.maxStack = 30;
			item.rare = ItemRarityID.White;
			item.value = Item.buyPrice(copper: 25);

			item.useTime = item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.EatingUsing;

			item.useTurn = true;
			item.consumable = true;

			item.buffTime = 900;
			item.buffType = BuffID.Spelunker;

			item.UseSound = SoundID.Item3;
		}
        public override void OnConsumeItem(Player player) => CombatText.NewText(player.getRect(), Color.Orange, "Spelunker [30s]", true, false);	
    }
}
