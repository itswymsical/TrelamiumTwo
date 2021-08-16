using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Consumable.Food
{
	public class Elderberry : ModItem
	{
		public override string Texture => Assets.Items.Food + "Elderberry";
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

			Item.healMana = 12;
			Item.potion = true;

			Item.buffTime = 900;
			Item.buffType = BuffID.ManaRegeneration;

			Item.UseSound = SoundID.Item3;
		}
        public override void OnConsumeItem(Player player) => CombatText.NewText(player.getRect(), Color.BlueViolet, "Mana Regeneration [30s]", true, false);	
    }
}
