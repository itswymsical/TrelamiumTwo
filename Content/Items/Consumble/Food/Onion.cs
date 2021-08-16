using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Consumable.Food
{
	public class Onion : ModItem
	{
		public override string Texture => Assets.Items.Food + "Onion";
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

			Item.buffTime = 450;
			Item.buffType = BuffID.Endurance;

			Item.UseSound = SoundID.Item3;
		}
        public override void OnConsumeItem(Player player) => CombatText.NewText(player.getRect(), Color.BlueViolet, "Damage Reduction [15s]", true, false);	
    }
}
