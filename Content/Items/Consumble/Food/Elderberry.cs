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
			item.width = item.height = 20;
			item.maxStack = 30;
			item.rare = ItemRarityID.White;
			item.value = Item.buyPrice(copper: 25);

			item.useTime = item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.EatingUsing;

			item.useTurn = true;
			item.consumable = true;

			item.healMana = 12;
			item.potion = true;

			item.buffTime = 900;
			item.buffType = BuffID.ManaRegeneration;

			item.UseSound = SoundID.Item3;
		}
        public override void OnConsumeItem(Player player) => CombatText.NewText(player.getRect(), Color.BlueViolet, "Mana Regeneration [30s]", true, false);	
    }
}
